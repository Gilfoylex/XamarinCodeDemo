using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestCarouselViewPage : ContentPage
    {
        public TestCarouselViewPage()
        {
            InitializeComponent();
        }

        private void VisualElement_OnSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"CarouselView sizeChanged ==== {CarouselView.Height}");
        }

        private void TestCarouselViewPage_OnSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"TestPage height  ============{Height}");
        }
    }
}