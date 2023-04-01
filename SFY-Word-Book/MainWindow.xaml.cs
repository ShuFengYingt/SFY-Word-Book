using CommunityToolkit.Mvvm.Messaging;
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
using System.Windows.Media.Animation;
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

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            /*这一部分声明动画的内容*/
            //声明双精度 动画
            DoubleAnimation animation = new DoubleAnimation();
            //根据BeginAnimation设置的依赖属性，在原有基础上-30，
            animation.By = -30;

            ////设置动画初始值为按钮宽度
            //animation.From = Button_1.Width;
            ////结束值为宽度-30
            //animation.To = Button_1.Width - 30;

            //持续两秒
            animation.Duration = TimeSpan.FromSeconds(1);
            //设置往返执行
            animation.AutoReverse = true;
            //设置执行周期
            animation.RepeatBehavior = new RepeatBehavior(2);

             
            /* 执行动画,传入依赖属性以及动画实例 */
            Button_1.BeginAnimation(Button.WidthProperty, animation);
        }
    }
}
