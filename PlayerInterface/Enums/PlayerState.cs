using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerInterface.Enums
{
    public enum PlayerState
    {
        Unknown = 0,
        Preparing = 1,
        Running = 2,
        Stopping = 3,
        Stopped = 4,
        Buffering = 5,
    }
}
