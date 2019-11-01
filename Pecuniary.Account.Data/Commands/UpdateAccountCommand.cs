using System;
using System.Threading;
using EricBach.CQRS.Commands;
using MediatR;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Data.Commands
{
    public class UpdateAccountCommand : Command, IRequest<CancellationToken>
    {
        public UpdateAccountRequest Account { get; set; }

        public UpdateAccountCommand(Guid id, UpdateAccountRequest account) : base(id)
        {
            Account = account;
        }
    }
}
