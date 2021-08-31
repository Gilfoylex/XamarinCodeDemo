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
    public partial class GroupList : ContentPage
    {
        private GroupVm _vm = null;
        public GroupList()
        {
            InitializeComponent();
            BindingContext = _vm = new GroupVm();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            _vm.DEF.Add("6478364837");
        }
    }
}