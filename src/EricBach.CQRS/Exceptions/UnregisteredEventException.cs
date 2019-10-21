using System;

namespace EricBach.CQRS.Exceptions
{
    public class UnregisteredEventException : Exception
    {
        public UnregisteredEventException(string message) : base(message)
        {
        }
    }
}