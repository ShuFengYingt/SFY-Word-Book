using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Shared.Dtos
{
    public class RegisterUserDto : BaseDto
    {
        private int id;
        private string userName;
        private string email;
        private string password;
        private string account;
        private DateTime createDate;
        private string repeatPassword;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        public string RepeatPassword
        {
            get { return repeatPassword; }
            set { repeatPassword = value; OnPropertyChanged(); }
        }

        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; OnPropertyChanged(); }
        }
    }
}
