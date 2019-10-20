using System;
using CQRS.Common.Aggregate;
using CQRS.Common.EventHandlers;
using Pecuniary.Events.Account;
using Pecuniary.ViewModels;
using ISnapshot = CQRS.Events.EventStore.Snapshots.ISnapshot;
using Snapshot = CQRS.Events.EventStore.Snapshots.Snapshot;

namespace Pecuniary.WebApi.AccountCommand.Models
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
