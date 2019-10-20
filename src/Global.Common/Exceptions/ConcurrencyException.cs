using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Common.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message)
        {
        }
    }
}
