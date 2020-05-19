using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    public class InvalidSheetForImportException : NotLoggableException
    {
        public InvalidSheetForImportException()
        {
        }

        public InvalidSheetForImportException(string message) : base(message)
        {
        }

        public InvalidSheetForImportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSheetForImportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
