using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class LearningHistoryViewModel:BindableBase
    {
        public LearningHistoryViewModel() 
        {
            IsChecked = true;
        }


        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; RaisePropertyChanged(); }
        }
    }
}
