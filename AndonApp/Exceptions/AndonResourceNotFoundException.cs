using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Exception when a request to Andon fails because a referenced resource
    /// (such as a station) can't be found in the system.
    /// </summary>
    public class AndonResourceNotFoundException : AndonAppException
    {
        public AndonResourceNotFoundException() : base()
        {
        }

        public AndonResourceNotFoundException(string message) : base(message)
        {
        }

        public AndonResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
