﻿using System;
using EricBach.CQRS.EventRepository.Snapshots;

namespace Pecuniary.Account.Data.Models
{
    public class AccountSnapshot : Snapshot
    {
        public string Name { get; set; }

        public AccountSnapshot()
        {
        }

        public AccountSnapshot(Guid id, string name, int version, int eventVersion, DateTime timestamp)
        {
            Id = id;
            Name = name;
            Version = version;
            EventVersion = eventVersion;
            Timestamp = timestamp;
        }
    }
}
