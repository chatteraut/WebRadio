using System.Collections.Generic;
using System.Collections.ObjectModel;
using MicroMvvm;
using PlayerInterface.Interfaces;
using WebRadio.StaticThings;

namespace WebRadio.ViewModel
{
    public interface IRadioChannelListViewModel
    {
        RadioChannelViewModel ActiveChannel { get; set; }
        IPlayer ActivePlayer { get; set; }
        ObservableCollection<RadioChannelViewModel> Channels { get; set; }
        List<string> Groups { get; }
        RelayCommand OpenAddChannelC { get; }
        RelayCommand PauseStream { get; }
        IEnumerable<IPlayer> Players { get; set; }
        RelayCommand PlayStreamStart { get; }
        RelayCommand RemoveActiveChannel { get; }
        RelayCommand<RadioChannelViewModel> RemoveCommand { get; }
        RelayCommand SaveDataC { get; }
        RadioChannelViewModel SelectedChannel { get; set; }
        SettingsViewModel Settings { get; }
        PlayStream ThePlayStream { get; }

        void Add(RadioChannelViewModel channel);
        void LoadChannels();
        bool NameIsUniqe(string value);
        void OpenAddChannel();
        void PauseActive();
        void PlayActive();
        void Remove(RadioChannelViewModel channel);
        void RemoveActive();
        void RemoveChannel(RadioChannelViewModel channelToRem);
        void SaveChannelList();
        void SaveData();
    }
}