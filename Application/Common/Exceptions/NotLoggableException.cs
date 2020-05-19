using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [Serializable]
    public class NotLoggableException : Exception
    {
        public NotLoggableException()
        {
        }

        public NotLoggableException(string message) : base(message)
        {
        }

        public NotLoggableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotLoggableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}