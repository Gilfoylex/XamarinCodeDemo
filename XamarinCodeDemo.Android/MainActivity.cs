using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using MediaManager;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace XamarinCodeDemo.Droid
{
    [Activity(Label = "XamarinCodeDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            FFImageLoading.ImageService.Instance.Initialize();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();

            // CrossMediaManager
            CrossMediaManager.Current.Init(this);

            NativeMedia.Platform.Init(this, savedInstanceState);
            LoadApplication(new App());
            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.Count == 1)
            {
                ToDeskTop();
            }
            else
            {
                Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
            }
        }

        private void ToDeskTop()
        {
            var intent = new Intent(Intent.ActionMain);
            intent.SetFlags(ActivityFlags.ClearTop);
            intent.AddCategory(Intent.CategoryHome);
            StartActivity(intent);
        }
    }
}