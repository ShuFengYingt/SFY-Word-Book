using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SFY_Word_Book.Api.Context
{
    /// <summary>
    /// 用户数据，数据传输层
    /// </summary>
    public class UserInfo
    {
        [Key]
        public string Account { get;set; }
        public int Id { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get;set; }

    }
}
