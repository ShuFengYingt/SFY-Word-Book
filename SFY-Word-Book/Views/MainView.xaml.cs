﻿using Prism.Regions;
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
using System.Windows.Shapes;
using Newtonsoft.Json;
using SFY_Word_Book.WordBook;
using System.IO;
using SFY_Word_Book.ViewModels;
using SFY_Word_Book.Extensions;
using Prism.Ioc;

namespace SFY_Word_Book.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IContainerProvider containerProvider;

        public MainView(IRegionManager regionManager,IContainerProvider containerProvider)
        {
            InitializeComponent();
            HomeView homeView = new HomeView();
            //HomeView homeView = new HomeView();
            //ContentControl.Content = homeView;
            #region 基础窗口交互
            Button_WindowMin.Click += (s, e) =>
            {
                WindowState = WindowState.Minimized;
            };
            Button_WindowMax.Click += (s, e) =>
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
            };
            Button_WindowClose.Click += (s, e) =>
            {

                //检查复习情况
                ToDayReviewWords.CheckOrSetBack();

                //保存单词本
                CET6.OutCET6WordBook();
                //保存生词本
                NewWordBook.OutNewWordBook();
                //保存待复习词本
                ReviewWordBook.OutReviewWordBook();
                //保存今日学习词本
                ToDayHasLearnBook.OutHasLearnWordBook();



                Close();
            };

            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }

            };
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            # endregion

            

            ////设置主页面xaml上下文
            DataContext = new MainViewModel(containerProvider,regionManager);

            //左侧菜单栏选中页面后，收回菜单栏
            MenuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.containerProvider = containerProvider;
        }





    }
}
