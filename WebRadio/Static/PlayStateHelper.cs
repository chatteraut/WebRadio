using PlayerInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRadio.Static
{
    public static class PlayStateHelper
    {
        public static string GetLocalisedPlayStateNameByString(PlayerState PlayState)
        {
            PlayerState playState = (PlayerState)PlayState;
            string playStateS = "-";

            switch (playState)
            {
                case PlayerState.Unknown:
                    playStateS = Resources.PlayStates.PlayStates.Undefiend;
                    break;
                case PlayerState.Stopped:
                    playStateS = Resources.PlayStates.PlayStates.Stopped;
                    break;
                case PlayerState.Running:
                    playStateS = Resources.PlayStates.PlayStates.Playing;
                    break;
                case PlayerState.Preparing:
                    playStateS = Resources.PlayStates.PlayStates.Transitioning;
                    break;
                case PlayerState.Stopping:
                    playStateS = Resources.PlayStates.PlayStates.Stopping;
                    break;
                case PlayerState.Buffering:
                    playStateS = Resources.PlayStates.PlayStates.Buffering;
                    break;
            }

            return playStateS;
        }
    }
}
