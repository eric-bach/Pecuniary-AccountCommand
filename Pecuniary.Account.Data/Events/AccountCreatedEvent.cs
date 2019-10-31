using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.Commands;

namespace Pecuniary.Account.Data.Events
{
    public class AccountCreatedEvent : Event
    {
        private const int _eventVersion = 1;

        public CreateAccount Account { get; internal set; } = new CreateAccount();

        public AccountCreatedEvent() : base(nameof(AccountCreatedEvent), _eventVersion)
        {
        }

        public AccountCreatedEvent(Guid id, CreateAccount account) : base(nameof(AccountCreatedEvent), _eventVersion)
        {
            Id = id;
            EventName = nameof(AccountCreatedEvent);
            
            Account = account;
        }
    }
}
