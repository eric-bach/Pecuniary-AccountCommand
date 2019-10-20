using System.Collections.Generic;
using CQRS.Common.Events;

namespace CQRS.Common.Aggregate
{
    public interface IAggregateRoot
    {
        IEnumerable<Event> GetUncommittedChanges();

        void MarkChangesAsCommitted();

        void LoadFromHistory(IEnumerable<Event> history);
    }
}
