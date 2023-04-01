using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

    public class MainViewModel:ObservableObject
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
            { name = value; OnPropertyChanged(); }

        }

        private string title;
        //如法炮制
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }



        public void Show(string content)
        {
            Name = "Is Changed";
            Title = "this is title";

            //给Token_1的地址发送一个string类型的值content
            WeakReferenceMessenger.Default.Send(content, "Token_1");
        }


    }
}
