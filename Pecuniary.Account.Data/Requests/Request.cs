using System;

namespace Pecuniary.Account.Data.Requests
{
    // TODO Move to base class
    public class Request
    {
        public Guid Id { get; set; }

        public Request(Guid id)
        {
            Id = id;
        }
    }
}