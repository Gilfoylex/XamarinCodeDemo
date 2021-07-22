using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlatformsHelper.ImageHelper
{
    public static partial class ImageHelper
    {
        public static Task<ImageSource> CompressImage(string path, int compressionPercentage)
        {
            return CompressImageInner(path, compressionPercentage);
        }
    }
}
