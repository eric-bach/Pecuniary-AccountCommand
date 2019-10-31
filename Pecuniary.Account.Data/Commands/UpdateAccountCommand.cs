using System;
using System.Threading;
using EricBach.CQRS.Commands;
using EricBach.CQRS.EventRepository;
using MediatR;

namespace Pecuniary.Account.Data.Commands
{
    public class UpdateAccountCommand : Command, IRequest<CancellationToken>
    {
        public UpdateAccount Account { get; set; }

        public UpdateAccountCommand(Guid id, UpdateAccount account, IEventRepository<Models.Account> eventRepository) : base(id)
        {
            Account = account;
        }
    }

    public class UpdateAccount
    {
        public string Name { get; set; }
    }
}
