using System;
using System.Runtime.Serialization;

namespace WebRadio.Exceptions
{
    [Serializable]
    internal class EntryEmptyException : Exception
    {
        public EntryEmptyException()
        {
        }

        public EntryEmptyException(string message) : base(message)
        {
        }

        public EntryEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntryEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}