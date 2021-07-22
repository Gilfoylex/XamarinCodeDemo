using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlatformsHelper.VideoHelper
{
    public partial class VideoHelper
    {
        public TimeSpan DurationTimeSpan { get; private set; } = TimeSpan.Zero;
        public Task SetVideoPath(string path)
        {
            return SetVideoPathInner(path);
        }

        public Task<ImageSource> GetFrameAtTime(long seconds)
        {
            return GetFrameAtTimeInner(seconds);
        }
    }
}
