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
    public partial class TestLoadUnloadPage : ContentPage
    {
        public TestLoadUnloadPage()
        {
            InitializeComponent();
        }

        private void LifecycleEffect_OnLoaded(object sender, EventArgs e)
        {
            Debug.WriteLine("LifecycleEffect_OnLoaded");
        }

        private void LifecycleEffect_OnUnloaded(object sender, EventArgs e)
        {
            Debug.WriteLine("LifecycleEffect_OnUnloaded");
        }

        private async void NewPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestLoadUnloadPage());
        }
    }
}