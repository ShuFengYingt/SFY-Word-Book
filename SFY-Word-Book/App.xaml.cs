using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using SFY_Word_Book;
using SFY_Word_Book.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModuleA;
using ModuleB;

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
            
        }

        //重写ConfigureModuleCatalog
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //加入模块主类，记得在项目中添加项目引用，同时引用命名空间，
            moduleCatalog.AddModule<ModuleAProfile>();
            moduleCatalog.AddModule < ModuleBProfile>();

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
