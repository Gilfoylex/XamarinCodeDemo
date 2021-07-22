using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using MediaManager;
using MediaManager.Forms;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace XamarinCodeDemo.Controls
{
    public class TVideoView : VideoView
    {
        private static readonly Lazy<TVideoView>
            Lazy = new Lazy<TVideoView>(() => new TVideoView());
        public static TVideoView Instance => Lazy.Value;

        private TVideoView()
        {
            IsVisible = false;
            AutoPlay = false;
            ShowControls = true;
        }
    }

    public class MyVideoView : Grid
    {
        public static readonly BindableProperty ThumbImageProperty =
            BindableProperty.Create(nameof(ThumbImage), typeof(ImageSource), typeof(MyVideoView), null);

        public ImageSource ThumbImage
        {
            get => (ImageSource)GetValue(ThumbImageProperty);
            set => SetValue(ThumbImageProperty, value);
        }

        public static readonly BindableProperty VideoUrlProperty =
            BindableProperty.Create(nameof(VideoUrl), typeof(string), typeof(MyVideoView), null);

        public string VideoUrl
        {
            get => (string)GetValue(VideoUrlProperty);
            set => SetValue(VideoUrlProperty, value);
        }


        private CachedImage _thumbImage;
        private ImageButton _playBtn;
        private TVideoView _videoView = TVideoView.Instance;

        private string _videoUrl;

        public MyVideoView()
        {
            _thumbImage = new CachedImage
            {
                Aspect = Aspect.AspectFit,
                RetryCount = 0,
                CacheType = CacheType.Memory
            };

            _playBtn = new ImageButton
            {
                Source = "play_video.png",
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            _playBtn.Clicked += (sender, args) =>
            {
                PlayVideo();
            };


            Children.Add(_thumbImage);
            Children.Add(_playBtn);
        }

        public async void PlayVideo()
        {
            await CrossMediaManager.Current.Stop();
            Children.Add(_videoView);
            _videoView.IsVisible = true;
            _videoView.ShowControls = false;
            var mediaItem = await CrossMediaManager.Current.Extractor.CreateMediaItem(_videoUrl);
            await CrossMediaManager.Current.Play(mediaItem);
            await CrossMediaManager.Current.Play();
            _videoView.ShowControls = true;
        }

        public async void StopVideo()
        {
            await CrossMediaManager.Current.Stop();
            _videoView.IsVisible = false;
            Children.Remove(_videoView);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ThumbImageProperty.PropertyName)
            {
                _thumbImage.Source = ThumbImage;
            }

            if (propertyName == VideoUrlProperty.PropertyName)
            {
                _videoUrl = VideoUrl;
            }
        }
    }
}
