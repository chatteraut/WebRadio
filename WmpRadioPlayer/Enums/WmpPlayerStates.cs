﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmpRadioPlayer.Enums
{
        public enum WmpPlayerStates
        {
            Undefiend = 0,
            Stopped = 1,
            Paused = 2,
            Playing = 3,
            ScanForward = 4,
            ScanReverse = 5,
            Buffering = 6,
            Waiting = 7,
            MediaEnded = 8,
            Transitioning = 9,
            Ready = 10,
            Reconnecting = 11
        }
}
