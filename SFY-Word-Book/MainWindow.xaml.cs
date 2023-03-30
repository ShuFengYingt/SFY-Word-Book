using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SFY_Word_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //上下文
            this. DataContext = new MainViewModel();

            //注册一个接受string类型参数的消息，地址为Token_1
            //接收位置、令牌token、方法委托
            Messenger.Default.Register<string>(this, "Token_1", Show);

        }
         
        void Show(string value)
        {
            MessageBox.Show(value);
        }
    }
}
