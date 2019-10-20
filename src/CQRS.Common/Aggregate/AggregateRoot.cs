using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Common.Events;
using Utils = Global.Common.Utilities.Utils;

namespace CQRS.Common.Aggregate
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        /// <summary>
        /// Loads and applies internal events
        /// </summary>
        /// <param name="history"></param>
        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
            }

            if (history.Any())
            {
                Version = history.Last().Version;
            }
            EventVersion = Version;
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic d = this;

            d.Handle(Utils.ChangeTo(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }
    }
}
