using ModuleA.ViewModles;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace ModuleA
{
    //接口IModule
    public class ModuleAProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //通过容器注册导航页面
            containerRegistry.RegisterForNavigation<ViewA,ViewAViewModle>();
        }
    }
}
