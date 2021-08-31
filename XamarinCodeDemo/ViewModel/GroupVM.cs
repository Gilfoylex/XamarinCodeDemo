using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamarinCodeDemo.ViewModel
{
    public class GroupVm : ViewModelBase
    {
        public ObservableCollection<List<string>> _all = new ObservableCollection<List<string>>();

        public ObservableCollection<List<string>> All
        {
            get => _all;
            set => SetProperty(ref _all, value);
        }

        public GroupVm()
        {
            ABC.Add("A");
            ABC.Add("B");
            ABC.Add("C");
            ABC.Add("D");
            ABC.Add("E");

            //num.Add("1");
            //num.Add("2");
            //num.Add("3");
            //num.Add("4");
            //num.Add("5");

            All.Add(ABC);
            All.Add(DEF);
            All.Add(XYZ);
            
        }

        public List<string> _ABC = new List<string>();

        public List<string> ABC
        {
            get => _ABC;
            set => SetProperty(ref _ABC, value);
        }


        public List<string> _DEF = new List<string>();

        public List<string> DEF
        {
            get => _DEF;
            set => SetProperty(ref _DEF, value);
        }

        public List<string> _XYZ = new List<string>();

        public List<string> XYZ
        {
            get => _XYZ;
            set => SetProperty(ref _XYZ, value);
        }
    }
}
