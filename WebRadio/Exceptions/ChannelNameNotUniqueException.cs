using System;
using System.Runtime.Serialization;

namespace WebRadio.Exceptions
{
    [Serializable]
    internal class ChannelNameNotUniqueException : Exception
    {
        public ChannelNameNotUniqueException()
        {
        }

        public ChannelNameNotUniqueException(string message) : base(message)
        {
        }

        public ChannelNameNotUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChannelNameNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}