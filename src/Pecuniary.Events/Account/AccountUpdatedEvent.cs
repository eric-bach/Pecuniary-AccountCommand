using CQRS.Common.Events;
using Pecuniary.ViewModels;

namespace Pecuniary.Events.Account
{
    public class AccountUpdatedEvent : Event
    {
        public AccountViewModel Account { get; internal set; } = new AccountViewModel();

        public AccountUpdatedEvent(AccountViewModel account) : base(nameof(AccountUpdatedEvent))
        {
            Account.Id = Id = account.Id;
            Account.EventName = account.EventName;
            Account.Name = account.Name;
            Account.AccountTypeCode = account.AccountTypeCode;
        }
    }
}
