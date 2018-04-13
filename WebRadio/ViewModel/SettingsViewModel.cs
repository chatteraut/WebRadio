using MicroMvvm;
using Persistence;
using Persistence.Mapper;
using Persistence.Model;
using System;
using System.Windows;
using WebRadio.StaticThings;

namespace WebRadio.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        public SettingsModel Settings { get; set; }
        public PlayStream _stream;
        public ISettingsDao _settingsDao;

        public int Width
        {
            get
            {
                return Settings.Width;
            }
            set
            {
                Settings.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return Settings.Height;
            }
            set
            {
                Settings.Height = value;
            }
        }

        public int Volume
        {
            get
            {
                return Settings.Volume;
            }
            set
            {
                if (Settings.Volume != value)
                {
                    Settings.Volume = value;
                    _stream.SetVolume(value);
                    RaisePropertyChanged("Volume");
                }
                if (_stream.GetVolume() != value)
                {
                    _stream.SetVolume(value);
                }
            }
        }

        public Guid PlayerGuid
        {
            get
            {
                return Settings.UsedPlayerGuid;
            }
            set
            {
                Settings.UsedPlayerGuid = value;
            }
        }

        public SettingsViewModel(PlayStream stream, ISettingsDao dao)
        {
            _settingsDao = dao;
            _stream = stream;
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            try
            {
                Settings = _settingsDao.LoadSettings();
            }
            catch (LoadFailedException)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
                Settings = new SettingsModel();
            }
        }

        public void SaveSettings()
        {
            _settingsDao.SaveSettings(Settings);
            Console.WriteLine("Deserialized Settings.. hopefully");
        }
    }
}
