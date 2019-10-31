using System;
using System.Threading;
using MediatR;

namespace Pecuniary.Account.Data.Requests
{
    public class UpdateAccountRequest : Request, IRequest<CancellationToken>
    {
        public string Name { get; set; }

        public UpdateAccountRequest(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}
