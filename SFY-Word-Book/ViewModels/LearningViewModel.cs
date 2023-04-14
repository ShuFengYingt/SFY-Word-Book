using Prism.Common;
using Prism.Mvvm;
using SFY_Word_Book.Common.Commands;
using SFY_Word_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_Word_Book.Extensions;
using System.Runtime.InteropServices;
using SFY_Word_Book.ViewModles;
using Microsoft.VisualStudio.Services.Profile;

namespace SFY_Word_Book.ViewModels
{
    public class LearningViewModel:BindableBase
    {

        public LearningViewModel()
        {
            //初始化
            WordCards = new ObservableCollection<WordCard>();
            
            ShowTransKnowCommand = new Command(ShowTransFromKnow);
            ShowTransUnknowCommand = new Command(ShowTransFromUnknow);
            ToNextWordCommand = new Command(ToNextWord);
            CET4 = MainViewModel.CET4;
            IsKnowButtonShow = true;
            IsNextButtonShow = false;
            WordRank = 1;

            CreateWordCard();


        }

        /// <summary>
        /// 词书迁移
        /// </summary>
        private CET4 CET4 { get; set; }


        private ObservableCollection<WordCard> wordCards;
        /// <summary>
        /// 单词卡
        /// </summary>
        public ObservableCollection<WordCard> WordCards
        {
            get { return wordCards; }
            set { this.wordCards = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 单词序号
        /// </summary>
        private int WordRank { get; set; }

        /// <summary>
        /// 单词卡后台对象
        /// </summary>
        private WordCard TempWordCard { get; set; }

        /// <summary>
        /// 创建单词卡后台对象，从单词书中完成迁移
        /// </summary>
        private void TempWordCreate()
        {
            TempWordCard = new WordCard
            {
                Word = CET4.words[WordRank].headWord,
                PhoneticSymbol = "[" + CET4.words[WordRank].content.word.content.ukphone + "]",


            };


        }

        /// <summary>
        /// 创建单词卡
        /// </summary>
        private void CreateWordCard()
        {
            TempWordCreate();
            wordCards.Clear();
            wordCards.Add(TempWordCard);

        }

        /// <summary>
        /// 显示单词释义
        /// </summary>
        private void CreateTranslation()
        {
            for (int i = 0; i < CET4.words[WordRank].content.word.content.trans.Count; i++)
            {
                WordCards[WordCards.Count - 1].Translations.Add(new WordCard.Translation
                {
                    TransCN = CET4.words[WordRank].content.word.content.trans[i].tranCn,
                    PartOfSpeech = CET4.words[WordRank].content.word.content.trans[i].pos + "."
                });
            }
        }



        private bool isKnowButtonShow;
        /// <summary>
        /// 是否展示认识与不认识两个按钮
        /// </summary>
        public bool IsKnowButtonShow
        {
            get { return isKnowButtonShow; }
            set { SetProperty (ref isKnowButtonShow, value); }
        }

        private bool isNextButtonShow;
        /// <summary>
        /// 是否展示下一个个按钮
        /// </summary>
        public bool IsNextButtonShow
        {
            get { return isNextButtonShow; }
            set { SetProperty (ref isNextButtonShow, value); }
        }


        public Command ShowTransKnowCommand { get; set; }
        public void ShowTransFromKnow()
        {
            //显示释义
            CreateTranslation();
            //隐藏认识按钮
            IsKnowButtonShow = false;
            //显示下一个按钮
            IsNextButtonShow = true;

            //通知更新
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));
        }
        public Command ShowTransUnknowCommand { get; set; }
        public void ShowTransFromUnknow()
        {
            //显示释义
            CreateTranslation();
            //隐藏认识按钮
            IsKnowButtonShow = false;
            //显示下一个按钮
            IsNextButtonShow = true;

            //通知更新
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));

        }

        public Command ToNextWordCommand { get; set; }
        public void ToNextWord()
        {
            //序列增加
            WordRank++;
            //新单词卡
            CreateWordCard();

            //按钮显示控制
            IsNextButtonShow = false;
            isKnowButtonShow = true;

            //通知更新
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));


        }




    }
}
