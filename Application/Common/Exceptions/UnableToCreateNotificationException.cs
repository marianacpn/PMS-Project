using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    public class UnableToCreateNotificationException : WarningException
    {
        public UnableToCreateNotificationException()
        {
        }

        public UnableToCreateNotificationException(string message) : base(message)
        {
        }

        public UnableToCreateNotificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnableToCreateNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
