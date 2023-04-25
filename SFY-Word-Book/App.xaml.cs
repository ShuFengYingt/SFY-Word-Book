using Microsoft.Extensions.Configuration;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using SFY_Word_Book.Service;
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

        protected override void OnInitialized()
        {
            //获得弹窗
            var dialog = Container.Resolve<IDialogService>();

            //弹窗
            dialog.ShowDialog("LoginView", callback =>
            {
                //如果异常，则关闭
                if (callback.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                    return;
                }


            });
            base.OnInitialized();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoginService, LoginService>();

            //依赖注入，注册子页面导航,基于PrismApplication，containerRegistry.RegisterForNavigation<View, ViewModel>();
            containerRegistry.RegisterForNavigation<HomeView,HomeViewModel> ();
            containerRegistry.RegisterForNavigation<NewWordBookView, NewWordBookViewModel>();
            containerRegistry.RegisterForNavigation<LearningHistoryView, LearningHistoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<LearningView,LearningViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<ReviewView, ReviewViewModel>();

            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
        }
    }
}
