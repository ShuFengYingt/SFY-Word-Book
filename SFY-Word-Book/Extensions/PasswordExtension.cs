using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SFY_Word_Book.Extensions
{
    /// <summary>
    /// 密码框实现
    /// </summary>
    public class PasswordExtension
    {

        //自己实现密码框数据绑定
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordExtension), new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        /// <summary>
        /// 注册变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var password = sender as PasswordBox;
            string passwordValue = (string)e.NewValue;

            if (password != null && password.Password != passwordValue)
            {
                password.Password = passwordValue;
            }
        }


    }

    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociateObject_PasswordChanged;
        }

        private void AssociateObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string passwordValue = PasswordExtension.GetPassword(passwordBox);
            if (passwordValue != null && passwordBox.Password != passwordValue )
            {
                PasswordExtension.SetPassword(passwordBox, passwordValue);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociateObject_PasswordChanged;

        }
    }
}
