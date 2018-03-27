using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Generic catch-all exception when a request to Andon fails.
    /// </summary>
    public class AndonAppException : Exception
    {

        /// <summary>
        /// Generic catch-all exception when a request to Andon fails.
        /// </summary>
        public AndonAppException() : base()
        {
        }

        /// <summary>
        /// Generic catch-all exception when a request to Andon fails.
        /// </summary>
        public AndonAppException(string message) : base(message)
        {
        }

        /// <summary>
        /// Generic catch-all exception when a request to Andon fails.
        /// </summary>
        public AndonAppException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
