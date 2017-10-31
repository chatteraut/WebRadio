using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WebRadio.Static;
using WebRadio.ViewModel;

namespace WebRadio
{
    /// <summary>
    /// Interaction logic for AddRadioChannel.xaml
    /// </summary>
    public partial class AddRadioChannel : Window
    { 

        public AddRadioChannel(RadioChannelListViewModel lvm)
        {
            InitializeComponent();
            AddViewModel context = (AddViewModel)DataContext;
            context.RadioChannelList = lvm;
            context.FinishedEditing = Confirm;
            _groups.ItemsSource = lvm.Groups;
        }

        private void Confirm()
        {
            AddViewModel context = (AddViewModel)DataContext;
            string name = context.RadioChannel.Name;
            string savePath = Path.Combine(SavePathsHelper.PathBase, "Images");
            SavePathsHelper.TryCreateSaveFolder(savePath);
            savePath = Path.Combine(savePath, name + ".bmp");
            BitmapImage source = (BitmapImage)_image.Source;
            if (source != null)
            {
                SaveImageHelper.SaveImageToFile(savePath, source);
                context.RadioChannel.Image_Url = savePath;
            }     
            Close();
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] paths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (paths.Length != 1)
                {
                    MessageBox.Show(WebRadio.Resources.AddRadio.AddRadioStrings.Only_One_Image);
                }
                else
                {
                    string path = paths[0];
                    try
                    {
                        _image.Source = new BitmapImage(new Uri(path));
                    }
                    catch(NotSupportedException)
                    {
                        MessageBox.Show(WebRadio.Resources.AddRadio.AddRadioStrings.Only_One_Image);
                    }
                }
            }
        }
    }
}
