using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    public class UnableToCreateLogException : WarningException
    {
        public UnableToCreateLogException()
        {
        }

        public UnableToCreateLogException(string message) : base(message)
        {
        }

        public UnableToCreateLogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnableToCreateLogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
