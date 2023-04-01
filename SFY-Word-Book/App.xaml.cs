using Prism.DryIoc;
using Prism.Ioc;
using SFY_Word_Book;
using SFY_Word_Book.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SFY_Word_Book
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //基于Prism框架的WPF，
        //1。引入prism，
        //2，于app。xaml中引入prism命名空间，删去StartUpUI（如果有）
        //3，将app。cs的基类改为PrismApplication
        //4.重写方法

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册导航
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<ViewC>();

            
        }


    }
}
