using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.ViewModels;

namespace Pecuniary.Account.Data.Events
{
    public class AccountCreatedEvent : Event
    {
        public AccountViewModel Account { get; internal set; } = new AccountViewModel();

        public AccountCreatedEvent() : base(nameof(AccountCreatedEvent))
        {
        }

        public AccountCreatedEvent(Guid id, AccountViewModel account) : base(nameof(AccountCreatedEvent))
        {
            Id = id;
            EventName = nameof(AccountCreatedEvent);

            Account.Name = account.Name;
            Account.AccountTypeCode = account.AccountTypeCode;
        }
    }
}
