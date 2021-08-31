using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NativeMedia;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestGrallyPage : ContentPage
    {
        public TestGrallyPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            IMediaFile[] files = null;

            try
            {
                var request = new MediaPickRequest(2, MediaFileType.Image)
                {
                    PresentationSourceBounds = System.Drawing.Rectangle.Empty,
                    Title = "Select"
                };

                cts.CancelAfter(TimeSpan.FromMinutes(5));

                var results = await MediaGallery.PickAsync(request, cts.Token);
                files = results?.Files?.ToArray();
            }
            catch (OperationCanceledException)
            {
                // handling a cancellation request
            }
            catch (Exception)
            {
                // handling other exceptions
            }
            finally
            {
                cts.Dispose();
            }


            if (files == null)
                return;

            foreach (var file in files)
            {
                var fileName = file.NameWithoutExtension; //Can return an null or empty value
                var extension = file.Extension;
                var contentType = file.ContentType;
                await using var stream = await file.OpenReadAsync();
                //...
                file.Dispose();
            }
        }
    }
}