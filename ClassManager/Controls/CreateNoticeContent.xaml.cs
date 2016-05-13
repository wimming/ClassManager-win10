using ClassManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Controls
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class CreateNoticeContent : Page
    {
        private OrganizationViewModel OVM;
        private StorageFile file = null;

        public CreateNoticeContent ()
		{
			this.InitializeComponent();

            OVM = OrganizationViewModel.Instance;
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {

            // 设置 file picker.
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // 打开 file picker.
            file = await openPicker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    image.Source = bitmapImage;
                }
            }
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> settingData = new Dictionary<string, string>();
        }

        private async void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                await new Windows.UI.Popups.MessageDialog("请选择一张图片").ShowAsync();
                return;
            }

            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            Stream stream = fileStream.AsStream();

            UserViewModel.Instance.uploadImage(stream, file.Name);
        }

        public async System.Threading.Tasks.Task<Stream> getImageStream()
        {
            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            Stream stream = fileStream.AsStream();
            return stream;
        }
        public string getName ()
		{
			return name.Text;
		}
		public string getContent ()
		{
			return content.Text;
        }
        public double getDeadline()
        {
            return (new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day,
                timePicker.Time.Hours, timePicker.Time.Minutes, timePicker.Time.Seconds).AddHours(-8) - DateTime.Parse("1970-1-1")).TotalMilliseconds;
        }
    }
}
