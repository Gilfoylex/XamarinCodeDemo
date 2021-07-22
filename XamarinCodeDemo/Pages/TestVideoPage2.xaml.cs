using System;
using MediaManager;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestVideoPage2 : ContentPage
    {
        public TestVideoPage2()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //var mediaItem = await CrossMediaManager.Current.Extractor.CreateMediaItem(@"file://" + x.FullPath);
            //await CrossMediaManager.Current.Play(mediaItem);

            //var tt = CrossMediaManager.Current.Duration;
            ////var mediaItem2 = await CrossMediaManager.Current.Extractor.CreateMediaItem("https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4");

            ////await CrossMediaManager.Current.Play("https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4");
            ////await CrossMediaManager.Current.Play(@"file://" + x.FullPath);
            //await CrossMediaManager.Current.Play();
        }
    }
}