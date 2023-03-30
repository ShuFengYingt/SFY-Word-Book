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
    
    public class MainViewModel :ViewModelBase
    {
        public MainViewModel()
        {
            ShowCommand = new MyCommand(Show);
            Name = "Hello";
            Title = "Title";
        }

        private string name;
        private string title;

        public string Name 
        {
            get { return name; }
            set 
            { 
                name = value;

                //ViewModelBase下的通知更新方法
                OnPropertyChanged();
            }

        }

        //如法炮制
        public string Title
        {
            get { return title; }
            set { title = value;OnPropertyChanged(); }
        }

        public MyCommand ShowCommand { get; set; }


        public void Show()
        {
            Name = "Is Changed";
            Title = "this is title";
            MessageBox.Show("asd");
        }


    }
}
