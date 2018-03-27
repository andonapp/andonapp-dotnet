using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Exception when a request to Andon fails because it's unauthorized.
    /// </summary>
    public class AndonUnauthorizedRequestException : AndonAppException
    {

        /// <summary>
        /// Exception when a request to Andon fails because it's unauthorized.
        /// </summary>
        public AndonUnauthorizedRequestException() : base()
        {
        }

        /// <summary>
        /// Exception when a request to Andon fails because it's unauthorized.
        /// </summary>
        public AndonUnauthorizedRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Exception when a request to Andon fails because it's unauthorized.
        /// </summary>
        public AndonUnauthorizedRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
