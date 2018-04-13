using Persistence.Model;
using System;
using System.IO;
using System.Runtime.Serialization;
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
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = File.Open(_path, FileMode.Open))
                {
                    settings = (SettingsModel)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not Found");
                settings = new SettingsModel();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not Found");
                settings = new SettingsModel();
            }
            catch (IOException)
            {
                throw new LoadFailedException();
            }
            catch (SerializationException)
            {
                throw new LoadFailedException();
            }
            catch (InvalidCastException)
            {
                settings = new SettingsModel();
            }
            catch (Exception e)
            {
                throw e;
            }
            return settings;
        }

        public void SaveSettings(SettingsModel settings)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = File.Open(_path, FileMode.Create))
                {

                    formatter.Serialize(stream, settings);
                }
            }
            catch (IOException)
            {
                throw new SaveFailedException();
            }
        }
    }
}
