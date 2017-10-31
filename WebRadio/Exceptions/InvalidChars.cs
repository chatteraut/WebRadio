using System;
using System.Runtime.Serialization;

namespace WebRadio.Exceptions
{
    [Serializable]
    internal class InvalidCharsException : Exception
    {
        public InvalidCharsException()
        {
        }

        public InvalidCharsException(string message) : base(message)
        {
        }

        public InvalidCharsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCharsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}