using PlayerInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmpRadioPlayer.Enums;

namespace WmpRadioPlayer.Mapper
{
    public static class WmpPlayerStatesToDefaultPlayerStatesMapper
    {
        public static PlayerState Map(WmpPlayerStates state)
        {
            switch (state)
            {
                case WmpPlayerStates.Undefiend:
                    return PlayerState.Unknown;
                    break;
                case WmpPlayerStates.Stopped:
                    return PlayerState.Stopped;
                    break;
                case WmpPlayerStates.Paused:
                    return PlayerState.Stopped;
                    break;
                case WmpPlayerStates.Playing:
                    return PlayerState.Running;
                    break;
                case WmpPlayerStates.ScanForward:
                    return PlayerState.Preparing;
                    break;
                case WmpPlayerStates.ScanReverse:
                    return PlayerState.Preparing;
                    break;
                case WmpPlayerStates.Buffering:
                    return PlayerState.Buffering;
                    break;
                case WmpPlayerStates.Waiting:
                    return PlayerState.Preparing;
                    break;
                case WmpPlayerStates.MediaEnded:
                    return PlayerState.Stopped;
                    break;
                case WmpPlayerStates.Transitioning:
                    return PlayerState.Preparing;
                    break;
                case WmpPlayerStates.Ready:
                    return PlayerState.Stopped;
                    break;
                case WmpPlayerStates.Reconnecting:
                    return PlayerState.Preparing;
                    break;
                default:
                    throw new NotSupportedException("Case " + state.ToString() + " is not supported for enum " + nameof(PlayerState));
            }
        }
    }
}
