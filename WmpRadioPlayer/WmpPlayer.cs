using PlayerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerInterface.Enums;
using WmpRadioPlayer.Enums;
using WmpRadioPlayer.Mapper;

namespace WmpRadioPlayer
{
    public class WmpPlayer : IPlayer
    {
        private WMPLib.WindowsMediaPlayer _player = new WMPLib.WindowsMediaPlayer();

        private bool Running { get; set; } = false;
        private Action<PlayerState> _stateChangeCallback;

        public int GetVolumeFrom0to100()
        {
            return _player.settings.volume;
        }

        public void Initialize(Action<PlayerState> StateChangeCallback)
        {
            _stateChangeCallback = StateChangeCallback;
            _player.PlayStateChange += StateChanged;
            _player.Buffering += BufferingChaned;
        }

        
        public void SetUrl(string url)
        {
            _player.URL = url;
        }

        public void SetVolumeFrom0to100(int volume)
        {
            _player.settings.volume = volume;
        }

        public void Start()
        {
            Running = true;
            _player.controls.play();
        }

        public void Stop()
        {
            Running = false;
            _player.controls.stop();
        }

        private void BufferingChaned(bool start)
        {
            if (start && Running)
            {
                Task.Delay(1000).Wait();
                Start();
            }
        }


        private void StateChanged(int state)
        {
            var status = (WmpPlayerStates)state;
            Console.WriteLine("STATE: " + status.ToString());
            
            _stateChangeCallback(WmpPlayerStatesToDefaultPlayerStatesMapper.Map(status));

            HandleSpecialStateCases(status);
                
        }

        private void HandleSpecialStateCases(WmpPlayerStates status)
        {
            if(status == WmpPlayerStates.Ready)
            {
                ApplyRunning();
            }
        }

        private void ApplyRunning()
        {
            if (Running)
            {
                Task.Delay(1000).Wait();
                _player.controls.play();
            }
            else
            {
                _player.controls.stop();
            }
        }
    }
}
