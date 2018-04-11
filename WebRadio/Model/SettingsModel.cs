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
        public int Volume { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Guid UsedPlayerGuid { get; set; }
    }
}
