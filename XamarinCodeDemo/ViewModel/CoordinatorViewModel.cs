using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCodeDemo.ViewModel
{
    public class CoordinatorViewModel : ViewModelBase
    {

        public CoordinatorViewModel(string str)
        {
            TestStr = str;
        }

        public CoordinatorViewModel()
        {
        }

        private string _testStr = "fuckkkk!!";

        public string TestStr
        {
            get => _testStr;
            set => SetProperty(ref _testStr, value);
        }
    }
}
