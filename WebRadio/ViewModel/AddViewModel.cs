using MicroMvvm;
using System;
using System.Collections.Generic;

namespace WebRadio.ViewModel
{
    public class AddViewModel : ObservableObject
    {
        public RadioChannelListViewModel RadioChannelList { get; set; }
        public RadioChannelViewModel RadioChannel { get; set; }

        public Action FinishedEditing { get; set; }

        public AddViewModel()
        {
            RadioChannel = new RadioChannelViewModel();
        }

        public void Save()
        {
            FinishedEditing();
            RadioChannelList.Add(RadioChannel);
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
    }
}
