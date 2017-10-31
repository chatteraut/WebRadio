using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRadio.Model
{
    [Serializable]
    public class SettingsModel
    {
        private int _volume;
        private int _width;
        private int _height;

        public int Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                _volume = value;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }
    }
}
