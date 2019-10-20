using System;
using CQRS.Common.Events;
using Pecuniary.ViewModels;

namespace Pecuniary.Events.Account
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
