using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebRadio.Model;
using WebRadio.Properties;

namespace WebRadio.Mapper
{
    public static class SettingsMapper
    {
        public static SettingsModel DeserializeSettings(string path)
        {
            SettingsModel settings = null;
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(path, FileMode.Open);
                settings = (SettingsModel)formatter.Deserialize(stream); 
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File not Found");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not Found");
            }
            catch (IOException)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
            catch(InvalidCastException)
            {

            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                stream?.Close();
            }
            return settings;
        }

        public static void SerializeSettings(string path, SettingsModel settings)
        {
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(path, FileMode.Create);
                formatter.Serialize(stream, settings);
            }
            catch (IOException e)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
            catch(Exception e)
            {
                stream?.Close();
            }
        }
    }
}
