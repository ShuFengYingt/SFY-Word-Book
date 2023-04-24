using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Shared.Dtos
{
    /// <summary>
    /// 用户数据实体
    /// </summary>
    public class UserDto:BaseUser
    {
        private int id;
        private string name;
        private string email;
        private string password;


        [Key]
        public int Id
        {
            get { return id; } set {  id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return name; } set { name = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return email; } set { email = value;OnPropertyChanged(); }
        }
        public string Password
        {
            get { return password; } set { password = value;OnPropertyChanged(); }
        }


    }
}
