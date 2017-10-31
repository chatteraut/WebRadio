using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WebRadio.Static
{
    public static class SaveImageHelper
    {
        public static void SaveImageToFile(string path, BitmapImage file)
        {
            FileStream fs = null;
            try {
                var encoder = new JpegBitmapEncoder(); // Or PngBitmapEncoder, or whichever encoder you want
                encoder.Frames.Add(BitmapFrame.Create(file));
                fs = new FileStream(path, FileMode.Create);
                encoder.Save(fs);
            }
            catch(IOException e)
            {
                throw e;
            }
            finally
            {
                fs?.Close();
            }

        }
    }
}
