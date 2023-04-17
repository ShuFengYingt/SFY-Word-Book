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
using SFY_Word_Book.WordBook;

namespace SFY_Word_Book.ViewModels
{
    public class LearningViewModel:BindableBase
    {

        public LearningViewModel()
        {
            //初始化
            WordCards = new ObservableCollection<WordCard>();
            WordGroups = new ObservableCollection<WordGroupItem>();

            NumOfGroup = 10;

            ShowTransKnowCommand = new Command(ShowTransFromKnow);
            ShowTransUnknowCommand = new Command(ShowTransFromUnknow);
            ToNextWordCommand = new Command(ToNextWord);
            FinishThisGroupCommand = new Command(FinishThisGroup);
            ToNextGroupCommand = new Command(ToNextGroup);

            CET6 = MainViewModel.CET6;
            IsKnowButtonShow = true;
            IsNextButtonShow = false;
            isFinishTen = false;
            isToNextGroup = false;
            WordRank = 0;

            CreateWordCard();


        }
        /// <summary>
        /// 一组多少个
        /// </summary>
        public int NumOfGroup { get; set; }

        /// <summary>
        /// 词书迁移
        /// </summary>
        private CET6 CET6 { get; set; }

        private ObservableCollection<WordGroupItem> wordGroups;
        /// <summary>
        /// 单词组，每完成一组单词进行一次文件读写
        /// </summary>
        public ObservableCollection<WordGroupItem> WordGroups
        {
            get { return wordGroups; }
            set { wordGroups = value; RaisePropertyChanged(); }
        }



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
        /// 单词序号,比词书中的单词rank低1
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
                Word = CET6.words[WordRank].headWord,
                PhoneticSymbol = "[" + CET6.words[WordRank].content.word.content.ukphone + "]",


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
            for (int i = 0; i < CET6.words[WordRank].content.word.content.trans.Count; i++)
            {
                WordCards[WordCards.Count - 1].Translations.Add(new WordCard.Translation
                {
                    TransCN = CET6.words[WordRank].content.word.content.trans[i].tranCn,
                    PartOfSpeech = CET6.words[WordRank].content.word.content.trans[i].pos + "."
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

        private bool isFinishTen;
        /// <summary>
        /// 是否展示十个一组下次继续
        /// </summary>
        public bool IsFinishTen
        {
            get { return isFinishTen; }
            set { SetProperty(ref isFinishTen, value); }
        }

        private bool isToNextGroup;
        /// <summary>
        /// 是否进行下一组
        /// </summary>
        public bool IsToNextGroup
        {
            get { return isToNextGroup; }
            set { SetProperty(ref isToNextGroup, value); }

        }

        /// <summary>
        /// 认识该单词
        /// </summary>
        public Command ShowTransKnowCommand { get; set; }
        /// <summary>
        /// 显示释义
        /// </summary>
        public void ShowTransFromKnow()
        {
            //显示释义
            CreateTranslation();

            //加入单词群组
            WordGroups.Add(new WordGroupItem { HeadWord = WordCards[0].Word });

            if ((WordRank + 1) % NumOfGroup == 0)
            {
                IsKnowButtonShow = false;
                IsNextButtonShow = false;
                isFinishTen = true;

                //通知更新
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(isFinishTen)));


            }
            else
            {

                //隐藏认识按钮
                IsKnowButtonShow = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

                //显示下一个按钮
                IsNextButtonShow = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));


            }

        }
        /// <summary>
        /// 不认识该单词
        /// </summary>
        public Command ShowTransUnknowCommand { get; set; }
        /// <summary>
        /// 不认识单词
        /// </summary>
        public void ShowTransFromUnknow()
        {
            //显示释义
            CreateTranslation();

            //加入单词群组
            WordGroups.Add(new WordGroupItem { HeadWord = WordCards[0].Word });


            //当记忆完十个之后
            if ((WordRank + 1) % NumOfGroup == 0)
            {


                IsKnowButtonShow = false;
                IsNextButtonShow = false;
                isFinishTen = true;

                //通知更新
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(isFinishTen)));



            }
            else
            {

                //隐藏认识按钮
                IsKnowButtonShow = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

                //显示下一个按钮
                IsNextButtonShow = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));

            }


        }
        /// <summary>
        /// 下一个单词命令
        /// </summary>
        public Command ToNextWordCommand { get; set; }
        /// <summary>
        /// 下一个单词
        /// </summary>
        public void ToNextWord()
        {



            isKnowButtonShow = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));


            IsNextButtonShow = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            //序列增加
            WordRank++;
            //新单词卡
            CreateWordCard();



        }

        public Command FinishThisGroupCommand { get;set; }
        public void FinishThisGroup()
        {
            IsToNextGroup = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));


            IsFinishTen = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsFinishTen)));

            for (int i = WordRank + 1 - NumOfGroup; i < WordRank;i++)
            {
                ReviewWordBook.ReviewWords.Add(CET6.words[i]);
            }


        }
        public Command ToNextGroupCommand { get; set; } 
        public void ToNextGroup()
        {
            WordRank++;
            CreateWordCard();

            IsToNextGroup = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));

            IsKnowButtonShow = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            WordGroups.Clear();

        }







    }
}
