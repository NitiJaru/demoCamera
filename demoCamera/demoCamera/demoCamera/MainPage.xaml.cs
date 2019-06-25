using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace demoCamera
{
    public partial class MainPage : ContentPage
    {
        private List<PhotoInfo> _list = new List<PhotoInfo>();

        public MainPage()
        {
            InitializeComponent();
            CrossMedia.Current.Initialize();
            takePicButton.Clicked += TakePicButton_Clicked;
            pickPicButton.Clicked += PickPicButton_Clicked;
            photoListView.ItemSelected += (sender, e) => { myImage.Source = (e.SelectedItem as PhotoInfo).Path; };
        }

        private async void TakePicButton_Clicked(object sender, EventArgs e)
        {
            var setting = new StoreCameraMediaOptions
            {
                CompressionQuality = 50
            };

            var response = await CrossMedia.Current.TakePhotoAsync(setting);
            if (response == null)
            {
                await DisplayAlert("Error", "Photo is null", "Okay");
                return;
            }

            _list.Add(new PhotoInfo { PhotoId = $"Picture_{_list.Count + 1}", Path = response.Path });
            photoListView.ItemsSource = _list.ToList();

            // Get base64
            using (MemoryStream ms = new MemoryStream())
            {
                response.GetStream().CopyTo(ms);
                var bytes = ms.ToArray();

                var base64 = Convert.ToBase64String(bytes);
            }
        }

        private async void PickPicButton_Clicked(object sender, EventArgs e)
        {
            var setting = new PickMediaOptions
            {
                CompressionQuality = 50
            };

            var response = await CrossMedia.Current.PickPhotoAsync(setting);
            if (response == null)
            {
                await DisplayAlert("Error", "Photo is null", "Okay");
                return;
            }

            _list.Add(new PhotoInfo { PhotoId = $"Picture_{_list.Count + 1}", Path = response.Path });
            photoListView.ItemsSource = _list.ToList();

            // Get base64
            using (MemoryStream ms = new MemoryStream())
            {
                response.GetStream().CopyTo(ms);
                var bytes = ms.ToArray();

                var base64 = Convert.ToBase64String(bytes);
            }
        }
    }

    public class PhotoInfo
    {
        public string PhotoId { get; set; }
        public string Path { get; set; }
    }
}
