using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Generic catch-all exception when a request to Andon fails.
    /// </summary>
    public class AndonAppException : Exception
    {
        public AndonAppException() : base()
        {
        }

        public AndonAppException(string message) : base(message)
        {
        }

        public AndonAppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AndonAppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
