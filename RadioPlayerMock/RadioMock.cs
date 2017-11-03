using PlayerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerInterface.Enums;
using PlayerInterface.Exceptions;
using System.Threading;

namespace RadioPlayerMock
{
    public class RadioMock : IPlayer
    {
        private Action<PlayerState> _callback = null;
        private int _volume = 0;
        private string _url;
        private PlayerState _state = PlayerState.Unknown;

        public PlayerState State
        {
            get
            {
                return _state;
            }
            set
            {
                Write(value.ToString());
                _state = value;
                _callback(value);
            }
        }

        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                Write("Volume set to " + value.ToString());
                _volume = value;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                Write("Url set to " + value.ToString());
                _url = value;
            }
        }

        public string Name {
            get
            {
                return "RadioMock";
            }
        }

        public int GetVolumeFrom0to100()
        {
            return Volume;
        }

        public void Initialize(Action<PlayerState> stateChangeCallback)
        {
            _callback = stateChangeCallback;
        }

        public void SetUrl(string url)
        {
            Url = url;
        }

        public void SetVolumeFrom0to100(int volume)
        {
            if(volume > 100 || volume < 0)
            {
                throw new VolumeOutOfRagenException();
            }
            Volume = volume;
            
        }

        public void Start()
        {
            State = PlayerState.Preparing;

            Thread.Sleep(1000);

            State = PlayerState.Running;
        }

        public void Stop()
        {
            State = PlayerState.Stopping;

            Thread.Sleep(1000);

            State = PlayerState.Stopped;
        }

        private void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
