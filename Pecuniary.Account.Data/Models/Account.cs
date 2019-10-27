using System;
using EricBach.CQRS.Aggregate;
using EricBach.CQRS.EventHandlers;
using Pecuniary.Account.Data.Events;
using Pecuniary.Account.Data.ViewModels;
using ISnapshot = EricBach.CQRS.EventStore.Snapshots.ISnapshot;
using Snapshot = EricBach.CQRS.EventStore.Snapshots.Snapshot;

namespace Pecuniary.Account.Data.Models
{
    public class Account : AggregateRoot, IEventHandler<AccountCreatedEvent>, IEventHandler<AccountUpdatedEvent>, ISnapshot
    {
        public string Name { get; set; }
        public string AccountTypeCode { get; set; }

        public Account()
        {
        }

        public Account(Guid id, AccountViewModel vm)
        {
            ApplyChange(new AccountCreatedEvent(id, vm));
        }

        public void UpdateAccount(AccountViewModel vm)
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
            AccountTypeCode = e.Account.AccountTypeCode;
        }

        public Snapshot GetSnapshot()
        {
            return new AccountSnapshot(Id, Name, Version, EventVersion);
        }

        public void SetSnapshot(Snapshot snapshot)
        {
            Name = ((AccountSnapshot) snapshot).Name;

            Id = snapshot.Id;
            Version = snapshot.Version;
            EventVersion = snapshot.EventVersion;
        }
    }
}
