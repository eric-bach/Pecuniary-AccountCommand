using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.ViewModels;

namespace Pecuniary.Account.Data.Events
{
    public class AccountCreatedEvent : Event
    {
        private const int _eventVersion = 1;

        public AccountViewModel Account { get; internal set; } = new AccountViewModel();

        public AccountCreatedEvent() : base(nameof(AccountCreatedEvent), _eventVersion)
        {
        }

        public AccountCreatedEvent(Guid id, AccountViewModel account) : base(nameof(AccountCreatedEvent), _eventVersion)
        {
            Id = id;
            EventName = nameof(AccountCreatedEvent);
            
            Account = account;
        }
    }
}
