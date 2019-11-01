using System;
using EricBach.CQRS.Requests;

namespace Pecuniary.Account.Data.Requests
{
    public class CreateAccountRequest : Request
    {
        public string Name { get; set; }

        public string AccountTypeCode { get; set; }

        public CreateAccountRequest(Guid id) : base(id)
        {
        }
    }
}
