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
                    PlayStream.DefaultInstance1.SetVolume(value);
                    RaisePropertyChanged("Volume");
                }
                if(PlayStream.DefaultInstance1.GetVolume() != value)
                {
                    PlayStream.DefaultInstance1.SetVolume(value);
                }
            }
        }

        public SettingsViewModel()
        {
            Settings = SettingsMapper.DeserializeSettings(Path.Combine(SavePathsHelper.PathBase, "Settings.sts"));
            if(Settings == null)
            {
                Settings = new SettingsModel();
            }
        }

        public void SaveSettings()
        {
            SettingsMapper.SerializeSettings(Path.Combine(SavePathsHelper.PathBase, "Settings.sts"), Settings);
            Console.WriteLine("Deserialized Settings.. hopefully");
        }
    }
}
