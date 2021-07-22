using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestMediaPicker : ContentPage
    {
        public TestMediaPicker()
        {
            InitializeComponent();
        }

        private async void ImagePick_OnClicked(object sender, EventArgs e)
        {
            var x = await FilePicker.PickMultipleAsync();
        }
    }
}