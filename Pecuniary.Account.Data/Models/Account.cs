﻿using System;
using EricBach.CQRS.Aggregate;
using EricBach.CQRS.EventHandlers;
using EricBach.CQRS.EventRepository.Snapshots;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Events;

namespace Pecuniary.Account.Data.Models
{
    public class Account : AggregateRoot, IEventHandler<AccountCreatedEvent>, IEventHandler<AccountUpdatedEvent>, ISnapshot
    {
        public string Name { get; set; }
        public string AccountTypeCode { get; set; }

        public Account()
        {
        }

        public Account(Guid id, CreateAccount vm)
        {
            ApplyChange(new AccountCreatedEvent(id, vm));
        }

        public void UpdateAccount(UpdateAccount vm)
        {
            ApplyChange(new AccountUpdatedEvent(Id, vm));
        }

        public void Handle(AccountCreatedEvent e)
        {
            Id = e.Id;
            Name = e.Account.Name;
            AccountTypeCode = e.Account.AccountTypeCode;
            
            Version = e.Version;
            EventVersion = e.EventVersion;
        }

        public void Handle(AccountUpdatedEvent e)
        {
            Id = e.Id;
            Name = e.Account.Name;
        }

        public Snapshot GetSnapshot()
        {
            return new AccountSnapshot(Id, Name, Version, EventVersion, Timestamp);
        }

        public void SetSnapshot(Snapshot snapshot)
        {
            Name = ((AccountSnapshot) snapshot).Name;

            Id = snapshot.Id;
            Version = snapshot.Version;
            EventVersion = snapshot.EventVersion;
            Timestamp = snapshot.Timestamp;
        }
    }
}
