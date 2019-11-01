using System;

namespace Pecuniary.Account.Data.Requests
{
    public class UpdateAccountRequest : Request
    {
        public string Name { get; set; }

        public UpdateAccountRequest(Guid id) : base(id)
        {
        }
    }
}
