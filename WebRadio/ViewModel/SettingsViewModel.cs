using MicroMvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebRadio.Mapper;
using WebRadio.Model;
using WebRadio.Static;
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
                if(Settings.Volume != value)
                {
                    Settings.Volume = value;
                    _stream.SetVolume(value);
                    RaisePropertyChanged("Volume");
                }
                if(_stream.GetVolume() != value)
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
            Settings = _settingsDao.LoadSettings();
            if (Settings == null)
            {
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
