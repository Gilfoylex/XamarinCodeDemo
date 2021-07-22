using Java.IO;
using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Xamarin.Forms;
using Console = System.Console;

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
                    var bitmap = BitmapFactory.DecodeFile(path);
                    if (bitmap == null) return null;

                    using var ms = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, compressionPercentage, ms);
                    return ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
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
