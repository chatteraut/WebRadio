using Persistence.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace WebRadio.Mapper
{
    public static class RadioChannelListMapper
    {
        public static List<RadioChannel> DeserializeRadioChannelList(string path)
        {
            List<RadioChannel> channels = new List<RadioChannel>();
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = File.Open(path, FileMode.Open))
                {
                    channels = (List<RadioChannel>)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
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
            catch (SerializationException)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
            catch (InvalidCastException)
            {

            }
            return channels;
        }

        public static void SerializeRadioChannelList(string path, List<RadioChannel> channels)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = File.Open(path, FileMode.Create))
                {
                    formatter.Serialize(stream, channels);
                }
            }
            catch (IOException)
            {
                MessageBox.Show(icon: MessageBoxImage.Error, messageBoxText: Resources.Strings.CannotOpenFile + " " + path, button: MessageBoxButton.OK, caption: Resources.Strings.Problem);
            }
        }
    }
}
