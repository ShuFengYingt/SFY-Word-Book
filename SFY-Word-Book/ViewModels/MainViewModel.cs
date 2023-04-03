using Prism.Mvvm;
using Prism.Regions;
using SFY_Word_Book.Common.Modles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModles
{
    public class MainViewModel:BindableBase
    {
        //需要传入区域接口，对接主页面上下文
        public MainViewModel(IRegionManager regionManager)
        {

            MenuBars = new ObservableCollection<MenuBar>();
            
            CreateMenuBar();

        }
        

        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        //菜单中有四个列
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "生词本", NameSpace = "NewWordBook" });
            MenuBars.Add(new MenuBar() { Icon = "CalendarRange", Title = "学习历史", NameSpace = "LearningHistory" });
            MenuBars.Add(new MenuBar() { Icon = "CogOutline", Title = "设置", NameSpace = "SettingsView" });

        }
    }
}
 