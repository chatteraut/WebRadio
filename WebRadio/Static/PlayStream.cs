﻿using MicroMvvm;
using PlayerInterface.Enums;
using PlayerInterface.Interfaces;
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
        
        private PlayerState _playState;
        private IPlayer _player;

        public PlayStream(IPlayer radioPlayer)
        {
            _player = radioPlayer;
            InitializePlayer();
        }

        public IPlayer RadioPlayer
        {
            get
            {
                return _player;
            }
            set
            {
                _player.Stop();
                _player = value;
                InitializePlayer();
            }
        }

        public PlayerState PlayState
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
                    RaisePropertyChanged(nameof(PlayState));
                }
            }
        }

        public void setRadio(string streamUrl)
        {
            _player.Stop();
            _player.SetUrl(streamUrl);
            _player.Start();
        }
        public void SetVolume(int volume)
        {
            _player.SetVolumeFrom0to100(volume);
        }

        public int GetVolume()
        {
            return _player.GetVolumeFrom0to100();
        }

        public void PlayTheStream()
        {
            _player.Start();
        }
        public void StopTheStream()
        {
            _player.Stop();
        }

        private void PlayStateChanges(PlayerState NewState)
        {
            PlayState = NewState;
        }

        private void InitializePlayer()
        {
            _player.Initialize(PlayStateChanges);
        }
    }
}