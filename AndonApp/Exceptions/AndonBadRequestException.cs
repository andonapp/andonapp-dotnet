using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Generic exception when a request to Andon fails because there's something wrong
    /// with the request.
    /// </summary>
    public class AndonBadRequestException : AndonAppException
    {
        public AndonBadRequestException() : base()
        {
        }

        public AndonBadRequestException(string message) : base(message)
        {
        }

        public AndonBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AndonBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
