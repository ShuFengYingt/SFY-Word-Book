using Prism.Mvvm;
using SFY_Word_Book.Common.Commands;
using SFY_Word_Book.Common.Models;
using SFY_Word_Book.WordBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class ReviewViewModel:BindableBase
    {
        public ReviewViewModel() 
        {
            //属性初始化
            NumOfGroup = 10;
            WordIndex = 0;
            IsKnowButtonShow = true;
            IsNextButtonShow = false;
            isFinishTen = false;
            isToNextGroup = false;

            #region 命令
            ShowTransKnowCommand = new Command(ShowTransFromKnow);
            ShowTransUnknowCommand = new Command(ShowTransFromUnknow);
            ToNextWordCommand = new Command(ToNextWord);
            FinishThisGroupCommand = new Command(FinishThisGroup);
            ToNextGroupCommand = new Command(ToNextGroup);
            #endregion

            #region 列表初始化
            WordCards = new ObservableCollection<WordCard>();
            WordGroups = new ObservableCollection<WordGroupItem>();
            #endregion

            #region 方法调用
            CreateWordCard();
            #endregion


        }

        /// <summary>
        /// 一组多少个
        /// </summary>
        public int NumOfGroup { get; set; }

        private ObservableCollection<WordGroupItem> wordGroups;
        /// <summary>
        /// 单词组，每完成一组单词进行一次文件读写，用于每组完成后的显示
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
        /// 当前单词索引
        /// </summary>
        private int WordIndex { get; set; }

        /// <summary>
        /// 单词卡后台对象
        /// </summary>
        private WordCard TempWordCard { get; set; }

        /// <summary>
        /// 创建单词卡后台对象，从ReviewWordBook中完成迁移
        /// </summary>
        private void TempWordCreate()
        {
            if (WordIndex >= ToDayReviewWords.TodayReviewWords.Count)
            {
                IsKnowButtonShow = false;
                TempWordCard = new WordCard
                {
                    Word = "啥也没有，这个判断得写主页",
                    PhoneticSymbol = "",
                    


                };
            }
            else
            {
                TempWordCard = new WordCard
                {
                    Word = ToDayReviewWords.TodayReviewWords[WordIndex].headWord,
                    PhoneticSymbol = "[" + ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.ukphone + "]",


                };

            }

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
            for (int i = 0; i < ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.trans.Count; i++)
            {
                WordCards[WordCards.Count - 1].Translations.Add(new WordCard.Translation
                {
                    TransCN = ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.trans[i].tranCn,
                    PartOfSpeech = ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.trans[i].pos + "."
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
            set { SetProperty(ref isKnowButtonShow, value); }
        }

        private bool isNextButtonShow;
        /// <summary>
        /// 是否展示下一个个按钮
        /// </summary>
        public bool IsNextButtonShow
        {
            get { return isNextButtonShow; }
            set { SetProperty(ref isNextButtonShow, value); }
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

            //加入单词群组，用于本组结束后的显示
            WordGroups.Add(new WordGroupItem { HeadWord = WordCards[0].Word });

            if ((WordIndex + 1) % NumOfGroup == 0)
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
            if ((WordIndex + 1) % NumOfGroup == 0)
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
            WordIndex++;
            //新单词卡
            CreateWordCard();



        }
        /// <summary>
        /// 本组结束命令
        /// </summary>
        public Command FinishThisGroupCommand { get; set; }
        /// <summary>
        /// 本组结束
        /// </summary>
        public void FinishThisGroup()
        {
            IsToNextGroup = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));


            IsFinishTen = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsFinishTen)));

            WordIndex++;

            WordBookMoveAndSet();
        }

        /// <summary>
        /// 对单词书的操作
        /// </summary>
        private void WordBookMoveAndSet()
        {
            for (int i = WordIndex - NumOfGroup;i < WordIndex;i++)
            {
                ToDayReviewWords.TodayReviewWords[i].DateTime = DateTime.Today;

                //设置下次复习时间
                if (ToDayReviewWords.TodayReviewWords[i].Combo == 1)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(2);
                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 2)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(4);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 3)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(7);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 4)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(15);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 5)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(30);

                }
                else//记忆完成
                {
                    ToDayReviewWords.TodayReviewWords[i].Combo = -1;

                }
                ToDayReviewWords.TodayReviewWords[i].Combo++;

                if (ToDayReviewWords.TodayReviewWords[i].Combo != -1)
                {
                    //添加至待复习
                    ReviewWordBook.ReviewWords.Add(ToDayReviewWords.TodayReviewWords[i]);

                }
                //添加至今日已学习
                ToDayHasLearnBook.ToDayHasLearnWords.Add(ToDayReviewWords.TodayReviewWords[i]);



            }
            for (int i = 0;i < WordIndex;i++)
            {
                ToDayReviewWords.TodayReviewWords.RemoveAt(0);
            }

            WordIndex = 0;
        }

        public Command ToNextGroupCommand { get; set; }
        /// <summary>
        /// 前往下一组单词
        /// </summary>
        public void ToNextGroup()
        {
            CreateWordCard();

            IsToNextGroup = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));

            IsKnowButtonShow = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            WordGroups.Clear();



        }











    }


}
