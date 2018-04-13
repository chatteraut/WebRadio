using Persistence.Model;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Persistence.Mapper
{
    public class SettingsFileDao : ISettingsDao
    {
        string _path;

        public SettingsFileDao(string path)
        {
            _path = path;
        }

        public SettingsModel LoadSettings()
        {
            SettingsModel settings = null;
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(_path, FileMode.Open);
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
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + _path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
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

        public void SaveSettings(SettingsModel settings)
        {
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(_path, FileMode.Create);
                formatter.Serialize(stream, settings);
            }
            catch (IOException e)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + _path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
            catch(Exception e)
            {
                stream?.Close();
            }
        }
    }
}
