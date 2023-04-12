using Prism.Common;
using Prism.Mvvm;
using SFY_Word_Book.Common.Commands;
using SFY_Word_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_Word_Book.Extensions;
using System.Runtime.InteropServices;

namespace SFY_Word_Book.ViewModels
{
    public class LearningViewModel:BindableBase
    {

        public LearningViewModel()
        {
            //初始化
            WordCards = new ObservableCollection<WordCard>();
            CreateWordCard();



        }


        private ObservableCollection<WordCard> wordCards;
        public ObservableCollection<WordCard> WordCards
        {
            get { return wordCards; }
            set { this.wordCards = value; RaisePropertyChanged(); }
        }




        void CreateWordCard()
        {
           CSolves.Sentence translation = CSolves.SentenceCreate(1, "fantic", "好！");

            WordCards.Add(new WordCard() { Word = translation.SentenceContent, PhoneticSymbol = translation.SentenceCN });
        }





    }
}
