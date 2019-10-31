using System;
using System.Threading;
using EricBach.CQRS.Commands;
using MediatR;

namespace Pecuniary.Account.Data.Commands
{
    public class CreateAccountCommand : Command, IRequest<CancellationToken>
    {
        public CreateAccount Account { get; set; }

        public CreateAccountCommand(Guid id, CreateAccount account) : base(id)
        {
            Account = account;
        }
    }

    public class CreateAccount
    {
        public string Name { get; set; }

        public string AccountTypeCode { get; set; }
    }
}
