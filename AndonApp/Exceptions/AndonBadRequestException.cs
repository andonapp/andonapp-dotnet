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

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// with the request.
        /// </summary>
        public AndonBadRequestException() : base()
        {
        }

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// with the request.
        /// </summary>
        public AndonBadRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// with the request.
        /// </summary>
        public AndonBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
