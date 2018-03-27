﻿using System;
using System.Runtime.Serialization;

namespace AndonApp.Exceptions
{

    /// <summary>
    /// Exception when a request to Andon fails because one of the inputs is invalid.
    /// </summary>
    public class AndonInvalidRequestException : AndonAppException
    {
        public AndonInvalidRequestException(): base()
        {
        }

        public AndonInvalidRequestException(string message) : base(message)
        {
        }

        public AndonInvalidRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
