using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class LoadFailedException : Exception
    {
        public LoadFailedException() : base()
        {

        }

        public LoadFailedException(string message) : base(message)
        {

        }

        public LoadFailedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
