using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Generic exception when a request to Andon fails because there's something wrong
    /// within Andon.
    /// </summary>
    public class AndonInternalErrorException : AndonAppException
    {
        public AndonInternalErrorException() : base()
        {
        }

        public AndonInternalErrorException(string message) : base(message)
        {
        }

        public AndonInternalErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AndonInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
