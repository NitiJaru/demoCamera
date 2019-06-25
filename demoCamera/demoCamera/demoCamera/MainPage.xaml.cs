using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace demoCamera
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            pickPicButton.Clicked += PickPicButton_Clicked;
        }

        private async void PickPicButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            var setting = new PickMediaOptions
            {
                CompressionQuality = 50
            };

            var response = await CrossMedia.Current.PickPhotoAsync(setting);
            myImage.Source = response?.Path;

            // Get base64
            using (MemoryStream ms = new MemoryStream())
            {
                response.GetStream().CopyTo(ms);
                var bytes = ms.ToArray();

                var base64 = Convert.ToBase64String(bytes);
            }
        }
    }
}
