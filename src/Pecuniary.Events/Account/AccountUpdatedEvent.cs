using System;
using CQRS.Common.Events;
using Pecuniary.ViewModels;

namespace Pecuniary.Events.Account
{
    public class AccountUpdatedEvent : Event
    {
        public AccountViewModel Account { get; internal set; } = new AccountViewModel();

        public AccountUpdatedEvent(Guid id, AccountViewModel account) : base(nameof(AccountUpdatedEvent))
        {
            Id = id;
            EventName = nameof(AccountUpdatedEvent);

            Account.Name = account.Name;
            Account.AccountTypeCode = account.AccountTypeCode;
        }
    }
}
