using System.Collections.Generic;
using EricBach.CQRS.Events;

namespace EricBach.CQRS.Aggregate
{
    public interface IAggregateRoot
    {
        IEnumerable<Event> GetUncommittedChanges();

        void MarkChangesAsCommitted();

        void LoadFromHistory(IEnumerable<Event> history);
    }
}
