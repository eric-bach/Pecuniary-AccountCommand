using System;

namespace CQRS.Events.EventStore.Snapshots
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
