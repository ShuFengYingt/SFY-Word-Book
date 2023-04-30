using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SFY_Word_Book.Extensions;
using SFY_Word_Book.Service;
using SFY_Word_Book.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SFY_Word_Book.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware//可弹窗
    {
        public LoginViewModel(ILoginService loginService)
        {
            registerUserDto = new RegisterUserDto();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
        }

        #region 接口实现
        public string Title { get; set; } = "我的单词书";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        #endregion

        private string account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private string password;
        private readonly ILoginService loginService;
          
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;
        /// <summary>
        /// 切换页面序号
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        private RegisterUserDto registerUserDto;
        /// <summary>
        /// 注册用户数据实体
        /// </summary>
        public RegisterUserDto RegisterUserDto
        {
            get { return registerUserDto; }
            set { registerUserDto = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        /// <summary>
        /// 导航界面方法
        /// </summary>
        /// <param name="arg"></param>
        private void Execute(string arg)
        {
            switch (arg)
            {
                case "Login":
                    {
                        Login();
                        break;
                    }
                case "LoginOut":
                    {
                        LoginOut();
                        break;
                    }
                    //跳转注册界面
                case "Go":
                    {
                        SelectedIndex = 1;
                        break;
                    }
                    //注册账号  
                case "Register":
                    {
                        Register();
                        break;
                    }
                    //返回登录界面
                case "Return":
                    {
                        SelectedIndex = 0;
                        break;
                    }

            }
        }

        /// <summary>
        /// 注册方法
        /// </summary>
        private async void Register()
        {
            //为空则不执行
            if (registerUserDto == null ||
                string.IsNullOrWhiteSpace(registerUserDto.Account) ||
                string.IsNullOrWhiteSpace(registerUserDto.UserName) ||
                string.IsNullOrWhiteSpace(registerUserDto.Password) ||
                string.IsNullOrWhiteSpace(registerUserDto.RepeatPassword))
            {
                return;
            }

            //输入密码不一致
            if (!registerUserDto.Password.Equals(registerUserDto.RepeatPassword))
            {
                //提示还没写
                return;
            }
            UserDto userDto = new UserDto();
            userDto.Account = registerUserDto.Account;
            userDto.UserName = registerUserDto.UserName;
            userDto.Password = registerUserDto.Password;

            var registerResult = await loginService.RegisterAsync(userDto);

            if (registerResult != null && registerResult.Statue)
            {
                //注册成功,返回
                SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("注册失败");
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            UserDto userDto = new UserDto();
            userDto.Account = UserName;
            userDto.UserName = UserName;
            userDto.Password = Password;
            var loginResult = await loginService.LoginAsync(userDto);

            //登录成功
            if (loginResult.Statue)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                //登录失败提示。。
                MessageBox.Show("登录失败");

            }


        }

        /// <summary>
        /// 退出
        /// </summary>
        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));

        }


    }
}
