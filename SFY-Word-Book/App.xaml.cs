using DryIoc;
using Prism.DryIoc;
using Prism.Services.Dialogs;
using Prism.Ioc;
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
using SFY_Word_Book.Common;

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
        public static void LoginOut(IContainerProvider containerProvider)
        {
            Current.MainWindow.Hide();

            var dialog = containerProvider.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                Current.MainWindow.Show();
            });
        }
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();

            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                var service = App.Current.MainWindow.DataContext as IConfigureService;
                if (service != null)
                {
                    service.Configure();
                }
                base.OnInitialized();
            });
            base.OnInitialized();

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册HttpRestClient
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:3333/", serviceKey: "webUrl");

            //注册服务
            containerRegistry.Register<ILoginService, LoginService>();
            //注册对话
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();


            //依赖注入，注册子页面导航,基于PrismApplication，containerRegistry.RegisterForNavigation<View, ViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<NewWordBookView, NewWordBookViewModel>();
            containerRegistry.RegisterForNavigation<LearningHistoryView, LearningHistoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<LearningView, LearningViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<ReviewView, ReviewViewModel>();
            containerRegistry.RegisterForNavigation<WordStoryView, WordStoryViewModel>();





        }
    }
}
