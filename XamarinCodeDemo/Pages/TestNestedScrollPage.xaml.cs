using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCodeDemo.ViewModel;

namespace XamarinCodeDemo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNestedScrollPage : ContentPage
    {
        public TestNestedScrollPage()
        {
            InitializeComponent();
            BindingContext = new CoordinatorViewModel();
        }
    }
}