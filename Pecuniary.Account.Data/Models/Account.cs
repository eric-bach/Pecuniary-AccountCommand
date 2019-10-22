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
            var accountViewModel = new AccountViewModel
            {
                Name = vm.Name,
                AccountTypeCode = vm.AccountTypeCode
            };

            ApplyChange(new AccountCreatedEvent(id, accountViewModel));
        }

        public void UpdateAccount(AccountViewModel vm, int version)
        {
            var accountViewModel = new AccountViewModel
            {
                Name = vm.Name,
                AccountTypeCode = vm.AccountTypeCode
            };

            ApplyChange(new AccountUpdatedEvent(Id, accountViewModel));
        }

        public void Handle(AccountCreatedEvent e)
        {
            Id = e.Id;
            Name = e.Account.Name;
            AccountTypeCode = e.Account.AccountTypeCode;
            
            Version = e.Version;
        }

        public void Handle(AccountUpdatedEvent e)
        {
            Id = e.Id;
            Name = e.Account.Name;
            AccountTypeCode = e.Account.AccountTypeCode;
        }

        public Snapshot GetSnapshot()
        {
            return new AccountSnapshot(Id, Name, Version);
        }

        public void SetSnapshot(Snapshot snapshot)
        {
            Name = ((AccountSnapshot) snapshot).Name;

            Id = snapshot.Id;
            Version = snapshot.Version;
        }
    }
}
