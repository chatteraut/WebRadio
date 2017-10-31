using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebRadio.Model;

namespace WebRadio.Mapper
{
    public static class RadioChannelListMapper
    {
        public static List<RadioChannel> DeserializeRadioChannelList(string path)
        {
            List<RadioChannel> channels = new List<RadioChannel>();
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(path, FileMode.Open);
                channels = (List<RadioChannel>)formatter.Deserialize(stream); 
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
            return channels;
        }

        public static void SerializeRadioChannelList(string path, List<RadioChannel> channels)
        {
            Stream stream = null;
            try
            {
                var formatter = new BinaryFormatter();
                stream = File.Open(path, FileMode.Create);
                formatter.Serialize(stream, channels);
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
