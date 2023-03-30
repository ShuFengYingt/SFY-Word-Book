using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SFY_Word_Book
{

    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            Name = "Hello";
            Title = "Title";
            ShowCommand = new RelayCommand<string>(Show);
        }

        public RelayCommand<string> ShowCommand { get; set; }

        private string name;
        public string Name
        { 
            get { return name; }
            set
            { name = value; RaisePropertyChanged(); }

        }

        private string title;
        //如法炮制
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }



        public void Show(string content)
        {
            Name = "Is Changed";
            Title = "this is title";
            MessageBox.Show(content);
        }


    }
}
