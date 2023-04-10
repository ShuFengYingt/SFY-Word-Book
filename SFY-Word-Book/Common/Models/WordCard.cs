using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    public class WordCard:BindableBase
    {
        public WordCard() { }

        /// <summary>
        /// 单词内容
        /// </summary>
        public string Word { get;set; }

        /// <summary>
        /// 音标
        /// </summary>
        public string PhoneticSymbol { get;set; }


    }
}
