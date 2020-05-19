using System;
using System.Runtime.Serialization;

namespace Shared.Support.Exceptions
{
    [Serializable]
    internal class InfoException : Exception
    {
        public InfoException()
        {
        }

        public InfoException(string message) : base(message)
        {
        }

        public InfoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InfoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}