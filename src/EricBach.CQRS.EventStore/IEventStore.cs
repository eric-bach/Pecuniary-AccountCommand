using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EricBach.CQRS.Aggregate;
using EricBach.CQRS.Events;
using EricBach.CQRS.EventStore.Snapshots;

namespace EricBach.CQRS.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId);

        Task SaveAsync(AggregateRoot aggregate);

        Task DeleteAllAsync();

        T GetSnapshot<T>(Guid aggregateId) where T : Snapshot;
        Task SaveSnapshotAsync(Snapshot snapshot);
    }
}
