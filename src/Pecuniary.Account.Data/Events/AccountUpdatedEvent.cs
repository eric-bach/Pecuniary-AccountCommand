using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.ViewModels;

namespace Pecuniary.Account.Data.Events
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
