using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    public class InvalidApplicationLogTypeException : Exception
    {
        public InvalidApplicationLogTypeException()
        {
        }

        public InvalidApplicationLogTypeException(string message) : base(message)
        {
        }

        public InvalidApplicationLogTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidApplicationLogTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
