using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    public class WordCard:BindableBase
    {
        public WordCard()
        {
            Translations = new ObservableCollection<Translation>();
        }

        /// <summary>
        /// 单词内容
        /// </summary>
        public string Word { get;set; }

        /// <summary>
        /// 音标
        /// </summary>
        public string PhoneticSymbol { get;set; }

        /// <summary>
        /// 下次复习时间
        /// </summary>
        public string ReviewDay { get; set; }

        private ObservableCollection<Translation> translations;
        public ObservableCollection<Translation> Translations
        {
            get { return translations; }
            set { translations = value; RaisePropertyChanged(); }
        }

        public bool IsShowTrans { get;set; }

        public class Translation
        {
            public string PartOfSpeech { get;set; }
            public string TransCN { get;set; }
        }

    }
}
