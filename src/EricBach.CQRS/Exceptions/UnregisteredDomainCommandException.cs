using System;

namespace EricBach.CQRS.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException(string message) : base(message)
        {
        }
    }
}