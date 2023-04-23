using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    public class WordGroupItem:BindableBase
    {
        /// <summary>
        /// 单词内容
        /// </summary>
        public string HeadWord { get; set; }
        /// <summary>
        /// 下次复习时间
        /// </summary>
        public string ReviewDay { get;set; }
    }
}
