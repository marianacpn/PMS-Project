using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    public class InvalidFileExtensionForSheetBackupException : Exception
    {
        public InvalidFileExtensionForSheetBackupException()
        {
        }

        public InvalidFileExtensionForSheetBackupException(string message) : base(message)
        {
        }

        public InvalidFileExtensionForSheetBackupException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFileExtensionForSheetBackupException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
