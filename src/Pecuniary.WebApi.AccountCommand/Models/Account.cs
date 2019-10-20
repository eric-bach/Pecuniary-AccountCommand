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
                Id = id,
                EventName = nameof(AccountCreatedEvent),
                Name = vm.Name,
                AccountTypeCode = vm.AccountTypeCode
            };

            ApplyChange(new AccountCreatedEvent(accountViewModel));
        }

        public void UpdateAccount(AccountViewModel vm, int version)
        {
            var accountViewModel = new AccountViewModel
            {
                Id = vm.Id,
                EventName = nameof(AccountUpdatedEvent),
                Name = vm.Name,
                AccountTypeCode = vm.AccountTypeCode
            };

            ApplyChange(new AccountUpdatedEvent(accountViewModel));
        }

        public void Handle(AccountCreatedEvent e)
        {
            Id = e.Account.Id;
            Name = e.Account.Name;

            Version = e.Version;
        }

        public void Handle(AccountUpdatedEvent e)
        {
            Name = e.Account.Name;
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
