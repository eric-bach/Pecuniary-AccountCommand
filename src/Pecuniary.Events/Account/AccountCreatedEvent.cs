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

        public AccountCreatedEvent(AccountViewModel account) : base(nameof(AccountCreatedEvent))
        {
            Account.EventName = nameof(AccountCreatedEvent);
            Account.Id = Id = account.Id;
            Account.Name = account.Name;
            Account.AccountTypeCode = account.AccountTypeCode;
        }
    }
}
