using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Data.Events
{
    public class AccountCreatedEvent : Event
    {
        private const int _eventVersion = 1;

        public CreateAccountRequest Account { get; internal set; }

        public AccountCreatedEvent() : base(nameof(AccountCreatedEvent), _eventVersion)
        {
        }

        public AccountCreatedEvent(Guid id, CreateAccountRequest account) : base(nameof(AccountCreatedEvent), _eventVersion)
        {
            Id = id;
            EventName = nameof(AccountCreatedEvent);
            
            Account = account;
        }
    }
}
