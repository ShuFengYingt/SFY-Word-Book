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
            CSolves.Sentence sentence_1 = CSolves.SentenceCreate(1, "fantic", "好！");
            CSolves.Sentence sentence1_2 = CSolves.SentenceCreate(2, "fantasy", "！");
            CSolves.Translation translation_1 = CSolves.TranslationCreate(1, "fantasy", "成功！");
            CSolves.Translation translation_2 = CSolves.TranslationCreate(2, "fantasy", "！");

            CSolves.Translation[] translations = new CSolves.Translation[] { translation_1, translation_2 };
            CSolves.Sentence[] sentences = new CSolves.Sentence[] { sentence_1, sentence1_2 };

            CSolves.Word word = CSolves.WordCreate(1, "fantasy", "好", "url", 0, false, 0, 2, sentences, 2, translations);
            CSolves.Word word_1 = CSolves.WordCreate(2, "fantastic", "好", "url", 0, false, 0, 2, sentences, 2, translations);
            CSolves.Word listWordHead = CSolves.WordListHeadCreate();
            CSolves.InsertWordToFront(listWordHead, word);
            CSolves.InsertWordToFront(listWordHead, word_1);

            string wordRank = CSolves.SearchByWordContent(listWordHead, "fantastic").ToString();
            Console.WriteLine(CSolves.SearchByWordRank(listWordHead, 1));
            WordCards.Add(new WordCard() { Word = CSolves.SearchByWordRank(listWordHead,1), PhoneticSymbol = word.Translations[0].TransCN });
        }





    }
}
