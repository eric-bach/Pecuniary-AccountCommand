﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Common.Aggregate;
using CQRS.Common.Events;
using CQRS.Events.EventStore.Snapshots;
using Global.Common.Exceptions;
using Global.Common.Utilities;

namespace CQRS.Events.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private List<Event> _events;
        private List<Snapshot> _snapshots;

        public InMemoryEventStore()
        {
            _events = new List<Event>();
            _snapshots = new List<Snapshot>();
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId)
        {
            var events = _events.Where(p => p.Id == aggregateId).Select(p => p);
            if (!events.Any())
            {
                throw new AggregateNotFoundException($"Aggregate with Id: {aggregateId} was not found");
            }

            return await Task.FromResult<IEnumerable<Event>>(events);
        }

        public Task SaveAsync(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in uncommittedChanges)
            {
                @event.Version = ++version;

                _events.Add(@event);

                // Save a snapshot every 3 events
                if (version > 2 && version % 3 == 0)
                {
                    var originator = (ISnapshot)aggregate;

                    var snapshot = originator.GetSnapshot();
                    snapshot.Version = version;

                    SaveSnapshotAsync(snapshot);
                }
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Utils.ChangeTo(@event, @event.GetType());

                // TODO Publish to SQS
                //_eventBus.Publish(desEvent);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await Task.FromResult<IEnumerable<Event>>(_events);
        }

        public Task DeleteAllAsync()
        {
            _events.Clear();

            return Task.CompletedTask;
        }

        public T GetSnapshot<T>(Guid aggregateId) where T : Snapshot
        {
            var snapshot = _snapshots.Where(m => m.Id == aggregateId).Select(m => m).LastOrDefault();

            return (T)snapshot;
        }

        public Task SaveSnapshotAsync(Snapshot snapshot)
        {
            _snapshots.Add(snapshot);

            return Task.CompletedTask;
        }
    }
}
