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

            List<Color> test = new List<Color>();
            test.Add(new Color() { Code = "#FFB6C1", Name = "浅粉红" });
            test.Add(new Color() { Code = "#FFC0CB", Name = "粉红" });
            test.Add(new Color() { Code = "#DC143C", Name = "深红" });

            this.list.ItemsSource = test;
        }
    }
    public class Color
    {
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
