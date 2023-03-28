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
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = this.Content as StackPanel;
            TextBox textBox = stackPanel.Children[0] as TextBox;

            if (string.IsNullOrEmpty(textBox.Name))
            {
                textBox.Text = "No name!";
            }
            else
            {
                textBox.Text = textBox.Name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
