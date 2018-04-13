using System;

namespace Persistence
{
    public class SaveFailedException : Exception
    {
        public SaveFailedException() : base()
        {

        }

        public SaveFailedException(string message) : base(message)
        {

        }

        public SaveFailedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
