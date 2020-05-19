using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class AuditableLogException : DomainException
    {
        public AuditableLogException()
        {
        }

        public AuditableLogException(string message) : base(message)
        {
        }

        public AuditableLogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuditableLogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
