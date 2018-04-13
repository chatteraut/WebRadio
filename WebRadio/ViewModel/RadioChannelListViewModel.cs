using MicroMvvm;
using Persistence;
using Persistence.Mapper;
using Persistence.Model;
using PlayerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using WebRadio.Exceptions;
using WebRadio.Kernel;
using WebRadio.Mapper;
using WebRadio.Static;
using WebRadio.StaticThings;

namespace WebRadio.ViewModel
{
    [Serializable]
    public class RadioChannelListViewModel : ObservableObject, IRadioChannelListViewModel
    {
        private RadioChannelViewModel _activeChannel;
        private SettingsViewModel settings;
        private PlayStream _playStream;

        public RadioChannelListViewModel()
        {
            var kernelProvider = KernelProvider.Instance;
            Players = kernelProvider.GetAll<IPlayer>().ToList();
            ISettingsDao settingsDao = kernelProvider.Get<ISettingsDao>();
            _playStream = new PlayStream(Players.First());
            Channels = new ObservableCollection<RadioChannelViewModel>();
            Settings = new SettingsViewModel(_playStream, settingsDao);
            LoadChannels();
            MainInstance = this;
        }

        public static RadioChannelListViewModel MainInstance;
        public ObservableCollection<RadioChannelViewModel> Channels { get; set; }
        public RadioChannelViewModel SelectedChannel { get; set; }
        public IList<IPlayer> Players { get; set; }

        public int ActivePlayerIndexOrZero
        {
            get
            {
                var guidPlayer = Players.Where(x => x.Identifier == Settings.PlayerGuid)
                    .FirstOrDefault();
                var pos = guidPlayer == null ? 0 : Players.IndexOf(guidPlayer);
                return pos;
            }
        }

        public IPlayer ActivePlayer
        {
            get
            {
                return _playStream.RadioPlayer;
            }
            set
            {
                ActiveChannel = null;
                _playStream.RadioPlayer = value;
                Settings.PlayerGuid = value.Identifier;
            }
        }

        public PlayStream ThePlayStream
        {
            get
            {
                return _playStream;
            }
        }

        public List<string> Groups
        {
            get
            {
                List<string> groups = new List<string>();
                foreach (RadioChannelViewModel channel in Channels)
                {
                    if (!groups.Contains(channel.Channel.Group_Name))
                    {
                        groups.Add(channel.Channel.Group_Name);
                    }
                }
                return groups;
            }
        }

        public RadioChannelViewModel ActiveChannel
        {
            get
            {
                return _activeChannel;
            }
            set
            {
                if (value == null)
                {
                    _activeChannel = null;
                    ThePlayStream.setRadio(null);
                    RaisePropertyChanged("ActiveChannel");
                }
                else if (_activeChannel != value)
                {
                    _activeChannel = value;
                    ThePlayStream.setRadio(value.Url);
                    ThePlayStream.SetVolume(Settings.Volume);
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

        public void Add(RadioChannelViewModel channel)
        {
            if (Channels.Contains(channel))
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
            if (Channels.Contains(channel))
            {
                Channels.Remove(channel);
                channel.Parent = null;
            }
            else
            {
                if (channel.Parent == this)
                {
                    channel.Parent = null;
                }
            }
        }

        public void PlayActive()
        {
            ThePlayStream.PlayTheStream();
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
            ThePlayStream.StopTheStream();
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
            ThePlayStream.StopTheStream();
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
            foreach (RadioChannel rc in channelList)
            {
                Add(new RadioChannelViewModel(rc));
            }
        }

        public void SaveData()
        {
            SavePathsHelper.TryCreateSaveFolder();
            SaveChannelList();
            try
            {
                Settings.SaveSettings();
            }
            catch (SaveFailedException)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
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
