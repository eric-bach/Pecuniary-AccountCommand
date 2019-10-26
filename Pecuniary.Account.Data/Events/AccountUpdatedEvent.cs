using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.ViewModels;

namespace Pecuniary.Account.Data.Events
{
    public class AccountUpdatedEvent : Event
    {
        private const int _eventVersion = 1;

        public AccountViewModel Account { get; internal set; }

        public AccountUpdatedEvent(Guid id, AccountViewModel account) : base(nameof(AccountUpdatedEvent), _eventVersion)
        {
            Id = id;
            EventName = nameof(AccountUpdatedEvent);

            Account = account;
        }
    }
}
