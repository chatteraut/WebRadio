using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRadio.Static
{
    public static class PlayStateHelper
    {
        public static string GetLocalisedPlayStateNameByString(int PlayState)
        {
            PlayStateE playState = (PlayStateE)PlayState;
            string playStateS = "-";

            switch (playState)
            {
                case PlayStateE.Undefiend:
                    playStateS = Resources.PlayStates.PlayStates.Undefiend;
                    break;
                case PlayStateE.Stopped:
                    playStateS = Resources.PlayStates.PlayStates.Stopped;
                    break;
                case PlayStateE.Paused:
                    playStateS = Resources.PlayStates.PlayStates.Paused;
                    break;
                case PlayStateE.Playing:
                    playStateS = Resources.PlayStates.PlayStates.Playing;
                    break;
                case PlayStateE.ScanForward:
                    playStateS = Resources.PlayStates.PlayStates.ScanForward;
                    break;
                case PlayStateE.ScanReverse:
                    playStateS = Resources.PlayStates.PlayStates.ScanBackward;
                    break;
                case PlayStateE.Buffering:
                    playStateS = Resources.PlayStates.PlayStates.Buffering;
                    break;
                case PlayStateE.Waiting:
                    playStateS = Resources.PlayStates.PlayStates.Waiting;
                    break;
                case PlayStateE.MediaEnded:
                    playStateS = Resources.PlayStates.PlayStates.MediaEnded;
                    break;
                case PlayStateE.Transitioning:
                    playStateS = Resources.PlayStates.PlayStates.Transitioning;
                    break;
                case PlayStateE.Ready:
                    playStateS = Resources.PlayStates.PlayStates.Ready;
                    break;
                case PlayStateE.Reconnecting:
                    playStateS = Resources.PlayStates.PlayStates.Reconnecting;
                    break;
            }

            return playStateS;
        }
    }

    public enum PlayStateE
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
