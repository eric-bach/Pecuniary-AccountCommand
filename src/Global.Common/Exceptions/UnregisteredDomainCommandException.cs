using System;

namespace Global.Common.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException(string message) : base(message)
        {
        }
    }
}