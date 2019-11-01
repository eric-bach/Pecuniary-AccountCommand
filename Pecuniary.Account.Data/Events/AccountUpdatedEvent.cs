using System;
using EricBach.CQRS.Events;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Data.Events
{
    public class AccountUpdatedEvent : Event
    {
        private const int _eventVersion = 1;

        public UpdateAccountRequest Account { get; internal set; }

        public AccountUpdatedEvent(Guid id, UpdateAccountRequest account) : base(nameof(AccountUpdatedEvent), _eventVersion)
        {
            Id = id;
            EventName = nameof(AccountUpdatedEvent);

            Account = account;
        }
    }
}
