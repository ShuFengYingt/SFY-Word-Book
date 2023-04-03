using MaterialDesignThemes.Wpf;
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

            /*UI交互层面，不涉及业务，无需使用绑定解耦*/
            //三大键
            Button_WindowMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            Button_WindowMax.Click += (s, e) =>
            { 
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;

                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }

            };
            Button_WindowClose.Click += (s, e) =>
            {
                this.Close();
            };

            //拖动窗口移动窗口
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            //双击窗口放大
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            };
        }

    }

    
}
