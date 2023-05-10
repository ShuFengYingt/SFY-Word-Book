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
using System.Configuration;
using Prism.DryIoc;
using System.Diagnostics;
using SFY_Word_Book.ViewModels;
using SFY_Word_Book.Common.Models;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System.Globalization;

namespace SFY_Word_Book.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ReadMoreButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        //private void AddToNew_Click(object sender, RoutedEventArgs e)
        //{
        //    var packIcon = FindVisualChild<PackIcon>(sender as Button, "PART_PackIcon");
        //    if (packIcon != null)
        //    {
        //        if (packIcon.Kind == PackIconKind.StarOutline)
        //        {
        //            packIcon.Kind = PackIconKind.Star;
        //        }
        //        else
        //        {
        //            packIcon.Kind = PackIconKind.StarOutline;
        //        }
        //    }
        //}
        //private T FindVisualChild<T>(DependencyObject parent, string name) where T : DependencyObject
        //{
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        var controlName = child.GetValue(Control.NameProperty) as string;
        //        if (controlName == name)
        //        {
        //            return child as T;
        //        }
        //        else
        //        {
        //            var result = FindVisualChild<T>(child, name);
        //            if (result != null)
        //                return result;
        //        }
        //    }
        //    return null;
        //}
    }

    
}
