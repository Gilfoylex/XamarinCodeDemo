using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCodeDemo.ViewModel;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestVideoPage : ContentPage
    {
        public TestVideoPage()
        {
            InitializeComponent();
            BindingContext = new MediaPreviewViewModel();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var mediaItem = await CrossMediaManager.Current.Extractor.CreateMediaItem("https://athena-voco-test.oss-cn-guangzhou.aliyuncs.com/Voco/bcc5ef80-fcb1-4f28-b16a-8dfb22737ef5");
            //CrossMediaManager.Current.MediaPlayer.VideoView = ff;
            await CrossMediaManager.Current.Play(mediaItem);
        }

        private void Ff_OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            Debug.WriteLine($"FFFGGGGGGGGGGGGG = {e.CurrentPosition}");
        }
    }
}