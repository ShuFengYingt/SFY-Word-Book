using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SFY_Word_Book.Common.Modles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_Word_Book.Extensions;
using System.Diagnostics;

namespace SFY_Word_Book.ViewModles
{
    public class MainViewModel : BindableBase
    {
        //需要传入区域接口，对接主页面上下文
        public MainViewModel(IRegionManager regionManager)
        {
            //实例化
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            //方法实现
            FowardAndBack();

        }

        //菜单在主页的声明和更新
        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 创建菜单中的四个列，绑定MenuBar类
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "HomeView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "生词本", NameSpace = "NewWordBookView" });
            MenuBars.Add(new MenuBar() { Icon = "CalendarRange", Title = "学习历史", NameSpace = "LearningHistoryView" });
            MenuBars.Add(new MenuBar() { Icon = "CogOutline", Title = "设置", NameSpace = "SettingsView" });



        }





        /* 导航功能模块 */
        /// <summary>
        /// 声明委托导航命令，传入MenuBar，服务于主页导航菜单
        /// </summary>
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        /// <summary>
        /// 上一步导航，用导航日志实现
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }

        /// <summary>
        /// 下一步导航，用导航日志实现
        /// </summary>
        public DelegateCommand GoForwardCommand { get; private set; }

        /// <summary>
        /// 用于设置主页面区域以及上下文
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// 区域导航日志，用于导航条上的“上一步”和“下一步”按钮
        /// </summary>
        private IRegionNavigationJournal journal;

        /// <summary>
        /// 导航方法
        /// 在regionManager.Regions下搜索key为PrismManager.MainViewRegionName
        /// 用RequestNavigate导航到menuBar的Namespace对应的页面
        /// </summary>
        /// <param name="menuBar"></param>
        private void Navigate(MenuBar menuBar)
        {
            //若menuBar或者其所在命名空间为创建或者丢失则禁止导航 
            if (menuBar == null || string.IsNullOrWhiteSpace(menuBar.NameSpace))
            {
                return;
            }

            //在regionManager.Regions下搜索key为PrismManager.MainViewRegionName，用RequestNavigate导航到menuBar的namespace对应的页面
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(menuBar.NameSpace, back =>
            {
                //用back.Context.NavigationService下的Journal属性实例化journal
                journal = back.Context.NavigationService.Journal;
            });



        }

        private void FowardAndBack()
        {
            //实现上一步功能
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });

            //实现下一步功能
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });

        }
    }
}
