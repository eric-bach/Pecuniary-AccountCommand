using System;
using System.Threading;
using EricBach.CQRS.Commands;
using MediatR;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Data.Commands
{
    public class CreateAccountCommand : Command, IRequest<CancellationToken>
    {
        public CreateAccountRequest Account { get; set; }

        public CreateAccountCommand(Guid id, CreateAccountRequest account) : base(id)
        {
            Account = account;
        }
    }
}
