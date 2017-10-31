using MicroMvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WebRadio.StaticThings
{

    public class PlayStream : ObservableObject
    {
        private static readonly PlayStream DefaultInstance = new PlayStream();

        #region Fields
        private WMPLib.WindowsMediaPlayer _player = new WMPLib.WindowsMediaPlayer();
        private int _playState;

        private PlayStream()
        {
            initialize();
        }

        public int PlayState
        {
            get
            {
                return _playState;
            }

            set
            {
                if (_playState != value)
                {
                    _playState = value;
                    RaisePropertyChanged("PlayState");
                }
            }
        }

        public static PlayStream DefaultInstance1
        {
            get
            {
                return DefaultInstance;
            }
        }
        #endregion Fields

        #region methods

        public void setRadio(string stream)
        {
            _player.controls.stop();
            _player.URL = stream;
        }
        public void SetVolume(int volume)
        {
            _player.settings.volume = volume;
        }

        public int GetVolume()
        {
            return _player.settings.volume;
        }

        public void PlayTheStream()
        {
            _player.controls.play();
        }
        public void StopTheStream()
        {
            _player.controls.stop();
        }
        #endregion methods

        #region listeners
        public void player_playStateChange(int NewState)
        {
            PlayState = NewState;
        }
        #endregion listeners

        private void initialize()
        {
            _player.PlayStateChange += player_playStateChange;
        }
    }
}