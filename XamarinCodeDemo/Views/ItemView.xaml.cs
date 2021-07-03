using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCodeDemo.Pages;

namespace XamarinCodeDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentView
    {
        public ItemView()
        {
            InitializeComponent();
        }

        private void ItemView_OnSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"ItemView_OnSizeChanged, ========== {Height}");
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InputPage());
        }
    }
}