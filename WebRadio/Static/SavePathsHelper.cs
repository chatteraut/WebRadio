using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebRadio.Static
{
    public static class SavePathsHelper
    {
        public static string PathBase
        {
            get
            {
                return Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "RadioPlayer");
            }
        }

        public static void TryCreateSaveFolder()
        {
            TryCreateSaveFolder(PathBase);
        }

        public static void TryCreateSaveFolder(string savePath)
        {
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CouldntCreateSaveFolder + " " + e.Message, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
        }

        public static bool DoesStringContainInvalidFileNameChars(string s)
        {
            foreach(char c in Path.GetInvalidFileNameChars())
            {
                if(s.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
