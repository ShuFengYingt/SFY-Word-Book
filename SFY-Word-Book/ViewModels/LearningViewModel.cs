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
using Microsoft.VisualStudio.Services.Profile;
using SFY_Word_Book.WordBook;
using System.Windows.Media;

namespace SFY_Word_Book.ViewModels
{
    public class LearningViewModel:BindableBase
    {

        public LearningViewModel()
        {
            #region 列表初始化
            WordCards = new ObservableCollection<WordCard>();
            WordGroups = new ObservableCollection<WordGroupItem>();
            #endregion
             
            #region 命令
            ShowTransKnowCommand = new Command(ShowTransFromKnow);
            ShowTransUnknowCommand = new Command(ShowTransFromUnknow);
            ToNextWordCommand = new Command(ToNextWord);
            FinishThisGroupCommand = new Command(FinishThisGroup);
            ToNextGroupCommand = new Command(ToNextGroup);
            FailToRememberCommand = new Command(FailToRemember);
            #endregion


            #region 属性初始化
            NumOfGroup = 10;
            IsKnowButtonShow = true;
            IsNextButtonShow = false;
            isFinishTen = false;
            isToNextGroup = false;
            IsFailToRememberShow = false;
            IsKnowButtonShow2 = false;
            WordIndex = 0;
            #endregion

            #region 方法调用
            ReadGroupWord();
            CreateWordCard();
            #endregion


        }
        /// <summary>
        /// 一组多少个
        /// </summary>
        public int NumOfGroup { get; set; }


        /// <summary>
        /// 发音api前置
        /// </summary>
        private string prePhoneSpeechUri = "https://dict.youdao.com/dictvoice?audio=";

        /// <summary>
        /// 背记的单词组
        /// </summary>
        private List<WordRoot.Root> groupWord = new List<WordRoot.Root>();
        /// <summary>
        /// 读取要背记的一组单词
        /// </summary>
        private void ReadGroupWord()
        {
            for (int i =0;i < NumOfGroup;i++)
            {
                groupWord.Add(LearningWordBook.LearningWords[i]);
            }
        }

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
        /// 创建单词卡后台对象，从LearningWordBook中完成迁移
        /// </summary>
        private void TempWordCreate()
        {
            TempWordCard = new WordCard
            {
                Word = groupWord[0].headWord,
                PhoneticSymbol = "[" + groupWord[0].content.word.content.ukphone + "]",
                PhoneSpeech = prePhoneSpeechUri + groupWord[0].content.word.content.ukspeech,


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
            for (int i = 0; i < groupWord[0].content.word.content.trans.Count; i++)
            {
                WordCards[WordCards.Count - 1].Translations.Add(new WordCard.Translation
                {
                    TransCN = groupWord[0].content.word.content.trans[i].tranCn,
                    PartOfSpeech = groupWord[0].content.word.content.trans[i].pos + "."
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
        private bool isFailToRememberShow;
        /// <summary>
        /// 是否显示记错了按钮
        /// </summary>
        public bool IsFailToRememberShow 
        {
            get { return  isFailToRememberShow; }

            set { SetProperty(ref isFailToRememberShow, value); }
        }
        private bool isKnowButtonShow2;
        /// <summary>
        /// 是否显示下一个2
        /// </summary>
        public bool IsKnowButtonShow2
        {
            get { return isKnowButtonShow2; }

            set { SetProperty(ref isKnowButtonShow2, value); }
        }


        /// <summary>
        /// 显示释义命令
        /// </summary>
        public Command ShowTransKnowCommand { get; set; }
        /// <summary>
        /// 认识单词
        /// </summary>
        public void ShowTransFromKnow()
        {
            //显示释义
            CreateTranslation();

            //加入单词群组，用于本组结束后的显示
            WordGroups.Add(new WordGroupItem { HeadWord = WordCards[0].Word });

            groupWord.RemoveAt(0);


            if (groupWord.Count == 0)
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

                //显示记错了按钮
                IsFailToRememberShow = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsFailToRememberShow)));


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


            //加到背记组最后一个
            groupWord.Add(groupWord[0]);
            groupWord.RemoveAt(0);

            //当记忆完十个之后
            if (groupWord.Count == 0)
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
                IsKnowButtonShow2 = true;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow2)));

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

            IsFailToRememberShow = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsFailToRememberShow)));

            IsKnowButtonShow2 = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow2)));


            //新单词卡
            CreateWordCard();



        }

        /// <summary>
        /// 记错了命令
        /// </summary>
        public Command FailToRememberCommand { get; set; }
        public void FailToRemember()
        {
            isKnowButtonShow = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsNextButtonShow)));


            IsNextButtonShow = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            IsFailToRememberShow = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsFailToRememberShow)));

            groupWord.Add(groupWord[0]);
            groupWord.RemoveAt(0);
            //新单词卡
            CreateWordCard();


        }
        /// <summary>
        /// 本组结束命令
        /// </summary>
        public Command FinishThisGroupCommand { get;set; }
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
        /// 对单词书的操作,迁移进待复习词书以及当日学习词书
        /// </summary>
        private void WordBookMoveAndSet()
        {
            //单词迁移进待复习词书以及当日学习词书
            for (int i = 0; i < NumOfGroup; i++)
            {
                //记录连对次数
                LearningWordBook.LearningWords[i].Combo++;
                //记录记忆时间
                LearningWordBook.LearningWords[i].DateTime = DateTime.Today;
                //下一次复习的时间,明日
                LearningWordBook.LearningWords[i].ReviewDays = DateTime.Today.AddDays(1);
                //加入待复习词书
                ReviewWordBook.ReviewWords.Add(LearningWordBook.LearningWords[i]);
                //今日已复习词书
                ToDayHasLearnBook.ToDayHasLearnWords.Add(LearningWordBook.LearningWords[i]);
            }


            //WordIndex归零，并删除LearningWords的内容
            for(int i = 0;i < 10;i++)
            {
                LearningWordBook.LearningWords.RemoveAt(0);
            }
            WordIndex = 0;

            LearningWordBook.OutLearningWordBook();

            
        }

        public Command ToNextGroupCommand { get; set; } 
        /// <summary>
        /// 前往下一组单词
        /// </summary>
        public void ToNextGroup()
        {
            ReadGroupWord();
            CreateWordCard();

            IsToNextGroup = false;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsToNextGroup)));

            IsKnowButtonShow = true;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsKnowButtonShow)));

            WordGroups.Clear();



        }
        








    }
}
