using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Common.Exceptions
{
    public class UnregisteredEventException : Exception
    {
        public UnregisteredEventException(string message) : base(message)
        {
        }
    }
}