using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Media;
using Xamarin.Forms;

namespace PlatformsHelper.VideoHelper
{
    public partial class VideoHelper
    {
        private MediaMetadataRetriever _mediaMetadataRetriever;
        internal Task SetVideoPathInner(string path)
        {
            _mediaMetadataRetriever = new MediaMetadataRetriever();
            return Task.Run(() =>
            {
                _mediaMetadataRetriever.SetDataSource(path);
                var durationString = _mediaMetadataRetriever.ExtractMetadata(MetadataKey.Duration);

                if (!string.IsNullOrEmpty(durationString) && long.TryParse(durationString, out var durationMs))
                    DurationTimeSpan = TimeSpan.FromMilliseconds(durationMs);
                else
                    DurationTimeSpan = TimeSpan.Zero;
            });
        }

        internal Task<ImageSource> GetFrameAtTimeInner(long seconds)
        {
            if (_mediaMetadataRetriever == null) return null;

            return Task.Run(() =>
            {
                var bitmap = _mediaMetadataRetriever.GetFrameAtTime(seconds * 1000 * 1000);
                return ToImageSource(bitmap);
            });
        }

        public static ImageSource ToImageSource(Bitmap bitmap)
        {
            if (bitmap == null) return null;

            using var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            var bitmapData = stream.ToArray();
            return ImageSource.FromStream(() => new MemoryStream(bitmapData));
        }
    }
}
