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

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// within Andon.
        /// </summary>
        public AndonInternalErrorException() : base()
        {
        }

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// within Andon.
        /// </summary>
        public AndonInternalErrorException(string message) : base(message)
        {
        }

        /// <summary>
        /// Generic exception when a request to Andon fails because there's something wrong
        /// within Andon.
        /// </summary>
        public AndonInternalErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
