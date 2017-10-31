﻿using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using WebRadio.Exceptions;
using WebRadio.Mapper;
using WebRadio.Model;
using WebRadio.Static;
using WebRadio.StaticThings;
using System.Windows.Controls;
using System.Windows;

namespace WebRadio.ViewModel
{
    [Serializable]
    public class RadioChannelListViewModel : ObservableObject
    {
        private RadioChannelViewModel _activeChannel;
        private SettingsViewModel settings;

        public static RadioChannelListViewModel MainInstance;

        public PlayStream ThePlayStream
        {
            get
            {
                return PlayStream.DefaultInstance1;
            }
        }
       

        public ObservableCollection<RadioChannelViewModel> Channels { get; set; }

        public List<string> Groups
        {
            get
            {
                List<string> groups = new List<string>();
                foreach(RadioChannelViewModel channel in Channels)
                {
                    if(!groups.Contains(channel.Channel.Group_Name))
                    {
                        groups.Add(channel.Channel.Group_Name);
                    }
                }
                return groups;
            }
        }

        public RadioChannelViewModel ActiveChannel {
            get
            {
                return _activeChannel;   
            }
            set
            {
                if (value == null)
                {
                    _activeChannel = null;
                    PlayStream.DefaultInstance1.setRadio(null);
                    RaisePropertyChanged("ActiveChannel");
                }
                else if (_activeChannel != value)
                {
                    _activeChannel = value;
                    PlayStream.DefaultInstance1.setRadio(value.Url);
                    PlayStream.DefaultInstance1.SetVolume(Settings.Volume);
                    RaisePropertyChanged("ActiveChannel");
                }
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return settings;
            }

            private set
            {
                settings = value;
            }
        }

        public RadioChannelViewModel SelectedChannel { get; set; }

       

        public RadioChannelListViewModel()
        {
            Channels = new ObservableCollection<RadioChannelViewModel>();
            Settings = new SettingsViewModel();
            LoadChannels();
            MainInstance = this;
        }

        public void Add(RadioChannelViewModel channel)
        {
            if(Channels.Contains(channel))
            {
                throw new ChannelNameNotUniqueException();
            }
            else
            {
                Channels.Add(channel);
                channel.Parent = this;
            }
        }

        public void Remove(RadioChannelViewModel channel)
        {
            if(Channels.Contains(channel))
            {
                Channels.Remove(channel);
                channel.Parent = null;
            }
            else
            {
                if(channel.Parent == this)
                {
                    channel.Parent = null;
                }
            }
        }

        public void PlayActive()
        {
            PlayStream.DefaultInstance1.PlayTheStream();
        }

        public RelayCommand PlayStreamStart
        {
            get
            {
                return new RelayCommand(PlayActive);
            }
        }

        public void PauseActive()
        {
            PlayStream.DefaultInstance1.StopTheStream();
        }

        public RelayCommand PauseStream
        {
            get
            {
                return new RelayCommand(PauseActive);
            }
        }

        public void RemoveActive()
        {
            PlayStream.DefaultInstance1.StopTheStream();
            ActiveChannel = null;
        }

        public RelayCommand RemoveActiveChannel
        {
            get
            {
                return new RelayCommand(RemoveActive);
            }
        }

        public void LoadChannels()
        {
            Channels.Clear();
            List<RadioChannel> channelList = RadioChannelListMapper.DeserializeRadioChannelList(Path.Combine(SavePathsHelper.PathBase, "RadioChannels.rcs"));
            foreach(RadioChannel rc in channelList)
            {
                Add(new RadioChannelViewModel(rc));
            }
        }

        public void SaveData()
        {
            SavePathsHelper.TryCreateSaveFolder();
            SaveChannelList();
            Settings.SaveSettings();
        }

        public void SaveChannelList()
        {
            List<RadioChannel> channelList = new List<RadioChannel>();
            foreach (RadioChannelViewModel channelVM in Channels)
            {
                channelList.Add(channelVM.Channel);
            }
            RadioChannelListMapper.SerializeRadioChannelList(Path.Combine(SavePathsHelper.PathBase, "RadioChannels.rcs"), channelList);
        }

        public RelayCommand SaveDataC
        {
            get
            {
                return new RelayCommand(SaveData);
            }
        }
        
        public void OpenAddChannel()
        {
            AddRadioChannel arc = new AddRadioChannel(this);
            arc.Show();
        }

        public RelayCommand OpenAddChannelC
        {
            get
            {
                return new RelayCommand(OpenAddChannel);
            }
        }

        public void RemoveChannel(RadioChannelViewModel channelToRem)
        {
            if (channelToRem != null)
            {
                string messageBoxcontent = String.Format(Resources.Strings.Really_Remove, channelToRem.Name);
                MessageBoxResult result = MessageBox.Show(messageBoxcontent, Resources.Strings.Problem, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Remove(channelToRem);
                }
            }
            else
            {
                MessageBox.Show(Resources.Strings.NoChannelSelected, Resources.Strings.Problem);
            }
        }

        public RelayCommand<RadioChannelViewModel> RemoveCommand
        {
            get
            {
                return new RelayCommand<RadioChannelViewModel>(RemoveChannel);
            }
        }

        public bool NameIsUniqe(string value)
        {
            foreach (RadioChannelViewModel rcvm in Channels)
            {
                if (value == rcvm.Name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
