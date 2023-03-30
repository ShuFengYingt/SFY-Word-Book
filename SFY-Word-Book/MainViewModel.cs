using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SFY_Word_Book
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            ShowCommand = new MyCommand(Show);
        }

        public MyCommand ShowCommand { get; set; }

        public void Show()
        {
            MessageBox.Show("asd");
        }
    }
}
