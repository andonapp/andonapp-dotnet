﻿using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Exception when a request to Andon fails because a referenced resource
    /// (such as a station) can't be found in the system.
    /// </summary>
    public class AndonResourceNotFoundException : AndonAppException
    {

        /// <summary>
        /// Exception when a request to Andon fails because one of the inputs is invalid.
        /// </summary>
        public AndonResourceNotFoundException() : base()
        {
        }

        /// <summary>
        /// Exception when a request to Andon fails because one of the inputs is invalid.
        /// </summary>
        public AndonResourceNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Exception when a request to Andon fails because one of the inputs is invalid.
        /// </summary>
        public AndonResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
