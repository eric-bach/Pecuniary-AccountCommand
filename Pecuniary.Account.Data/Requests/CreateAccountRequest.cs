using System;
using System.Threading;
using MediatR;

namespace Pecuniary.Account.Data.Requests
{
    public class CreateAccountRequest : Request, IRequest<CancellationToken>
    {
        public string Name { get; set; }

        public string AccountTypeCode { get; set; }

        public CreateAccountRequest(Guid id, string name, string accountTypeCode) : base(id)
        {
            Name = name;
            AccountTypeCode = accountTypeCode;
        }
    }
}
