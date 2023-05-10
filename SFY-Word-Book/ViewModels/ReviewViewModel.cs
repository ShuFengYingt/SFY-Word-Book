using ImTools;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SFY_Word_Book.ViewModels
{
    public class ReviewViewModel : BindableBase
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
            IsAllFinished = false;
            isSnackBarShow = false;

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
        /// 单词组，用于每组完成后的显示
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
        /// 发音api前置
        /// </summary>
        private string prePhoneSpeechUri = "https://dict.youdao.com/dictvoice?audio=";

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
                isAllFinished = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(isAllFinished)));

                IsToNextGroup = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));

                IsKnowButtonShow = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            }
            else
            {
                TempWordCard = new WordCard
                {
                    Word = ToDayReviewWords.TodayReviewWords[WordIndex].headWord,
                    PhoneticSymbol = "[" + ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.ukphone + "]",
                    PhoneSpeech = prePhoneSpeechUri + ToDayReviewWords.TodayReviewWords[WordIndex].content.word.content.ukspeech,


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

            //播放音频
            MediaPlayer phoneSpeechPlayer = new MediaPlayer();
            phoneSpeechPlayer.Open(new Uri(TempWordCard.PhoneSpeech));
            phoneSpeechPlayer.Play();


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

        private bool isAllFinished;
        public bool IsAllFinished
        {
            get { return isAllFinished; }
            set { SetProperty(ref isAllFinished, value); }
        }

        /// <summary>
        /// 认识该单词
        /// </summary>
        public Command ShowTransKnowCommand { get; set; }
        /// <summary>
        /// 认识，显示释义
        /// </summary>
        public void ShowTransFromKnow()
        {
            //显示释义
            CreateTranslation();


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

        private bool isSnackBarShow;
        /// <summary>
        /// 显示重置提示
        /// </summary>
        public bool IsSnackBarShow
        {
            get { return isSnackBarShow; }
            set { isSnackBarShow = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 异步显示重置提示
        /// </summary>
        /// <returns></returns>
        private async Task ShowSnackBarAsync()
        {
            IsSnackBarShow = true; // 显示snackBar
            await Task.Delay(2000); // 等待3秒
            IsSnackBarShow = false; // 隐藏snackBar
        }

        /// <summary>
        /// 不认识该单词
        /// </summary>
        public Command ShowTransUnknowCommand { get; set; }
        /// <summary>
        /// 不认识单词
        /// </summary>
        public async void ShowTransFromUnknow()
        {
            //显示释义
            CreateTranslation();

            //归一
            ToDayReviewWords.TodayReviewWords[WordIndex].Combo = 1;
            ShowSnackBarAsync();

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
            for (int i = WordIndex - NumOfGroup; i < WordIndex; i++)
            {
                ToDayReviewWords.TodayReviewWords[i].DateTime = DateTime.Today;
                //设置下次复习时间
                if (ToDayReviewWords.TodayReviewWords[i].Combo == 1)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt = 2;
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt);
                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 2)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt = 4;
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 3)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt = 7;
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 4)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt = 15;
                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(15);

                }
                else if (ToDayReviewWords.TodayReviewWords[i].Combo == 5)
                {
                    ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt = 30;

                    ToDayReviewWords.TodayReviewWords[i].ReviewDays = DateTime.Today.AddDays(ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt);

                }
                else//记忆完成
                {
                    ToDayReviewWords.TodayReviewWords[i].Combo = -1;

                }

                if (ToDayReviewWords.TodayReviewWords[i].Combo != -1)
                {
                    ToDayReviewWords.TodayReviewWords[i].Combo++;

                    //添加至待复习
                    ReviewWordBook.ReviewWords.Add(ToDayReviewWords.TodayReviewWords[i]);
                    WordGroups.Add(new WordGroupItem { HeadWord = ToDayReviewWords.TodayReviewWords[i].headWord, ReviewDay = ToDayReviewWords.TodayReviewWords[i].ReviewDaysInt.ToString() + "天" });

                }
                else
                {
                    WordGroups.Add(new WordGroupItem { HeadWord = ToDayReviewWords.TodayReviewWords[i].headWord, ReviewDay = "已完成" });

                }
                //添加至今日已学习
                ToDayHasLearnBook.ToDayHasLearnWords.Add(ToDayReviewWords.TodayReviewWords[i]);




            }
            for (int i = 0; i < WordIndex; i++)
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
            //如果没有了
            if (WordIndex >= ToDayReviewWords.TodayReviewWords.Count)
            {
                isAllFinished = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(isAllFinished)));

                IsToNextGroup = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));

                IsKnowButtonShow = false;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            }
            else
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


}
