using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Common.Aggregate;
using CQRS.Common.Events;
using CQRS.Events.EventStore.Snapshots;

namespace CQRS.Events.EventStore
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
