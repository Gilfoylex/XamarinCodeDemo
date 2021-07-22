using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace PlatformsHelper.ImageHelper
{
    public static partial class ImageHelper
    {
        internal static Task<ImageSource> CompressImageInner(string path, int compressionPercentage)
        {
            return Task.Run(() =>
            {
                try
                {
                    var image = UIImage.FromFile(path);
                    if (image == null) return null;

                    var quality = (nfloat) (compressionPercentage / 100.0);
                    var asStream = image.AsJPEG(quality).AsStream();
                    return ImageSource.FromStream(() => asStream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
    }
}
