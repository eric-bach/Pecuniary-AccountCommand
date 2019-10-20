using System;
using System.Threading;
using CQRS.Common.Commands;
using MediatR;
using Pecuniary.ViewModels;

namespace Pecuniary.Commands.Account
{
    public class CreateAccountCommand : Command, IRequest<CancellationToken>
    {
        public AccountViewModel Account { get; set; }

        public CreateAccountCommand(Guid id, AccountViewModel account) : base(id)
        {
            if (string.IsNullOrEmpty(account.Name))
                throw new Exception("Name is required");

            // TODO Figure out how to validate this from the "database" without making it a long synchronous call
            if (string.IsNullOrEmpty(account.AccountTypeCode) ||
                account.AccountTypeCode != "LIRA" && account.AccountTypeCode != "TFSA" &&
                account.AccountTypeCode != "RESP" && account.AccountTypeCode != "RRSP" &&
                account.AccountTypeCode != "Unreg")
                throw new Exception("Invalid AccountTypeCode. Must be one of values [LIRA, RESP, RRSP, TFSA, UnReg");

            Account = account;
        }
    }
}
