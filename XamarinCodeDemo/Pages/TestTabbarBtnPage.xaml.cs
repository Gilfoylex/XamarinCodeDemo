using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestTabbarBtnPage : ContentPage
    {
        public TestTabbarBtnPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            TabBarB.IsSelected = !TabBarB.IsSelected;
        }
    }
}