using Prism.DryIoc;
using Prism.Ioc;
using SFY_Word_Book.ViewModels;
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
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册子页面导航,基于PrismApplication，containerRegistry.RegisterForNavigation<View, ViewModel>();
            containerRegistry.RegisterForNavigation<HomeView,HomeViewModel> ();
            containerRegistry.RegisterForNavigation<NewWordBookView, NewWordBookViewModel>();
            containerRegistry.RegisterForNavigation<LearningHistoryView, LearningHistoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<LearningView,LearningViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
        }
    }
}
