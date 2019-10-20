using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Common.Aggregate;
using CQRS.Common.Events;
using CQRS.Events.EventStore;
using CQRS.Events.EventStore.Snapshots;
using Global.Common.Exceptions;
using Logger = Logging.LambdaLogger.Logger;

namespace CQRS.Events.Repository
{
    public class EventRepository<T> : IEventRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private static readonly object _lockStorage = new object();

        public EventRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            Logger.Log("Preparing to write aggregate to repository");

            if (aggregate.GetUncommittedChanges().Any())
            {
                lock (_lockStorage)
                {
                    var item = new T();

                    if (expectedVersion != 0)
                    {
                        item = GetById(aggregate.Id);
                        if (item.Version != expectedVersion)
                        {
                            throw new ConcurrencyException($"Aggregate {item.Id} has been previous modified");
                        }
                    }

                    _eventStore.SaveAsync(aggregate).Wait();
                }
            }
        }

        public T GetById(Guid id)
        {
            IEnumerable<Event> events;

            var snapshot = _eventStore.GetSnapshot<Snapshot>(id);

            if (snapshot != null)
            {
                events = _eventStore.GetEventsAsync(id).Result.Where(e => e.Version >= snapshot.Version);
            }
            else
            {
                events = _eventStore.GetEventsAsync(id).Result;
            }

            var obj = new T();
            if (snapshot != null)
            {
                ((ISnapshot)obj).SetSnapshot(snapshot);
            }

            obj.LoadFromHistory(events);
            return obj;
        }

        public void DeleteAll()
        {
            _eventStore.DeleteAllAsync();
        }
    }
}
