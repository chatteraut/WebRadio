using MicroMvvm;
using Persistence.Model;
using System;
using System.IO;
using WebRadio.Exceptions;
using WebRadio.Static;

namespace WebRadio.ViewModel
{
    [Serializable]
    public class RadioChannelViewModel : ObservableObject
    {
        private RadioChannelListViewModel _parent = null;

        public RadioChannel Channel { get; private set; }

        public RadioChannelViewModel()
        {
            Channel = new RadioChannel();
        }

        public RadioChannelViewModel(RadioChannel rc)
        {
            Channel = rc;
        }

        public RadioChannelListViewModel Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                RaisePropertyChanged("Parent");
                _parent = value;
            }
        }

        public string Name
        {
            get
            {
                return Channel.Name;
            }
            set
            {
                if (Channel.Name != value)
                {
                    if (!RadioChannelListViewModel.MainInstance.NameIsUniqe(value))
                    {
                        throw new ChannelNameNotUniqueException(Resources.AddRadio.AddRadioStrings.Name_not_unique);
                    }
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new EntryEmptyException(Resources.Strings.Field_Not_Emtpy);
                    }
                    if (SavePathsHelper.DoesStringContainInvalidFileNameChars(value))
                    {
                        throw new InvalidCharsException(Resources.AddRadio.AddRadioStrings.Invalid_chars + Path.GetInvalidFileNameChars().ToString());
                    }
                    RaisePropertyChanged("Name");
                    Channel.Name = value;
                }
            }
        }



        public string Url
        {
            get
            {
                return Channel.Url;
            }
            set
            {
                if (Channel.Url != value)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new EntryEmptyException(Resources.Strings.Field_Not_Emtpy);
                    }
                    RaisePropertyChanged("Url");
                    Channel.Url = value;
                }
            }
        }

        public string Image_Url
        {
            get
            {
                return Channel.Image_Url;
            }
            set
            {
                if (Channel.Image_Url != value)
                {
                    RaisePropertyChanged("Image_Url");
                    Channel.Image_Url = value;
                }
            }
        }

        public string Group_Name
        {
            get
            {
                return Channel.Group_Name;
            }
            set
            {
                if (Channel.Group_Name != value)
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new EntryEmptyException(Resources.Strings.Field_Not_Emtpy);
                    }
                    RaisePropertyChanged("Group_Name");
                    Channel.Group_Name = value;
                }
            }
        }



        private void SetActive(RadioChannelListViewModel channel)
        {
            channel.ActiveChannel = this;
        }

        public RelayCommand<RadioChannelListViewModel> ISetActive
        {
            get
            {
                return new RelayCommand<RadioChannelListViewModel>(SetActive);
            }
        }
    }
}
