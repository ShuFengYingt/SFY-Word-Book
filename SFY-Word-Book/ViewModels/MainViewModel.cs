using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SFY_Word_Book.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> OpenCommand { get;private set; }

        //IRegionManager接口管理订阅区域，记得创建字段（实例化）
        public MainViewModel(IRegionManager regionManager) 
        {
            OpenCommand = new DelegateCommand<string>(Open);
            this.regionManager = regionManager;
        }


      

        private void Open(string obj)
        {
            //首先通过IRegionManager接口获取到全局定义的“可用区域”
            //而后往这个区域动态地设置内容
            //设置内容的方式为通过依赖注入的形式
            
            regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }
    }
}
