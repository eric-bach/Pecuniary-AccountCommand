using System;
using EricBach.CQRS.EventStore.Snapshots;

namespace Pecuniary.Account.Data.Models
{
    public class AccountSnapshot : Snapshot
    {
        public string Name { get; set; }
        public int EventVersion { get; set; }

        public AccountSnapshot()
        {
        }

        public AccountSnapshot(Guid id, string name, int eventVersion)
        {
            Id = id;
            Name = name;
            EventVersion = eventVersion;
        }
    }
}
