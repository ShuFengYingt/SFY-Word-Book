using Microsoft.VisualBasic;
using Prism.Mvvm;
using SFY_Word_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Windows.Input;
using Prism.Commands;
using Microsoft.TeamFoundation.Common;
using Prism.Regions;
using SFY_Word_Book.Extensions;
using Prism.Modularity;
using System.Diagnostics;
using SFY_Word_Book.Common.Commands;
using SFY_Word_Book.Views;
using SFY_Word_Book.WordBook;
using System.Collections.Specialized;
using SFY_Word_Book.Common;

namespace SFY_Word_Book.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel(IRegionManager regionManager)
        {
            #region 初始化列表
            TaskBars = new ObservableCollection<TaskBar>();
            dailyPages = new ObservableCollection<DailyPage>();
            TransCNs = new ObservableCollection<SearchTransCN>();
            #endregion

            #region 命令
            AddToNewWordBookCommand = new Command(AddToNewWordBook);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            #endregion

            #region 属性初始化
            this.regionManage = regionManager;
            Random random = new Random();
            SelectedWordRank = random.Next(1, 2000);
            NumOfLearn = LearningWordBook.LearningWords.Count;
            NumOfReview = ReviewWordBook.ReviewWords.Count;
            NumOfHasLearn = ToDayHasLearnBook.ToDayHasLearnWords.Count;
            HeadTitle = $"欢迎回来！{Appsession.UserName}";

            #endregion
            ToDayHasLearnBook.ToDayHasLearnWords.CollectionChanged += OnTodayHasLearnWordBookCollectionChanged;
            ToDayReviewWords.TodayReviewWords.CollectionChanged += OnTodayReviewWordsCollectionChanged;
            LearningWordBook.LearningWords.CollectionChanged += OnLearningWordBookCollectionChanged;


            #region 单词查找用Words，迁移CET6
            //读入单词本
            CET6.ReadJson();

            //读取生词本
            NewWordBook.ReadJson();

            //读入待学习单词
            LearningWordBook.ReadJson();

            //读入当日学习的单词
            ToDayHasLearnBook.ReadJson();

            //读入待复习单词
            ReviewWordBook.ReadJson();

            //读入当日待复习单词
            ToDayReviewWords.ReadReviewWordBook();

            Words = CET6.words;
            #endregion

            #region 方法调用
            CreateDailyPage();
            #endregion
        }
        private string headTitle;
        public string HeadTitle
        {
            get { return headTitle; }
            set { headTitle = value; RaisePropertyChanged(); }
        }


        #region 任务栏

            //任务栏的通知更新
        private ObservableCollection<TaskBar> taskBars;
        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private int numOfReview;
        /// <summary>
        /// 待复习的词数
        /// </summary>
        public int NumOfReview
        {
            get { return numOfReview; }
            set { numOfReview = value; RaisePropertyChanged(); }
        }

        private int numOfLearn;
        /// <summary>
        /// 待学习个数
        /// </summary>
        public int NumOfLearn
        {
            get { return numOfLearn; }
            set { numOfLearn = value; RaisePropertyChanged(); }
        }

        private int numOfHasLearn;
        /// <summary>
        /// 当日学过的单词
        /// </summary>
        public int NumOfHasLearn
        {
            get { return numOfHasLearn; }
            set { numOfHasLearn = value; RaisePropertyChanged(); }
        }

        private void OnLearningWordBookCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                NumOfLearn = LearningWordBook.LearningWords.Count;
                UpdateTaskBar();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                NumOfLearn = LearningWordBook.LearningWords.Count;
                UpdateTaskBar();

            }
        }

        /// <summary>
        /// 订阅今日学习变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTodayHasLearnWordBookCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //若有增加
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                //先这样，以后再过滤哪些是今天学的，或者单独存
                NumOfHasLearn = ToDayHasLearnBook.ToDayHasLearnWords.Count;
                UpdateTaskBar();
            }
            //减少
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                NumOfHasLearn = ToDayHasLearnBook.ToDayHasLearnWords.Count;
                UpdateTaskBar();
            }
        }

        /// <summary>
        /// 订阅待复习变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTodayReviewWordsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                NumOfReview = ToDayReviewWords.TodayReviewWords.Count;
                UpdateTaskBar();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                NumOfReview = ToDayReviewWords.TodayReviewWords.Count;
                UpdateTaskBar();
            }
        }

        /// <summary>
        /// 待学习
        /// </summary>
        private TaskBar waitingToLearnTaskBar = new TaskBar()
        {
            Icon = "BookOpenPageVariant",
            Title = "待学习",
            Content = "2677",
            Color = "#48815a",
            NameSpace = "LearningView"
        };
        /// <summary>
        /// 待复习
        /// </summary>
        private TaskBar waitingToReiviewTaskBar = new TaskBar()
        {
            Icon = "BookClock",
            Title = "待复习",
            Content = "0",
            Color = "#008184",
            NameSpace = "ReviewView"
        };
        /// <summary>
        /// 已学习
        /// </summary>
        private TaskBar hasLearnedTaskBar = new TaskBar()
        {
            Icon = "BookCheck",
            Title = "已学习",
            Content = "0",
            Color = "#965fa0",
            NameSpace = "WordStoryView"
        };
        /// <summary>
        /// 词书
        /// </summary>
        private TaskBar typeOfBookTaskBar = new TaskBar()
        {
            Icon = "Bookshelf",
            Title = "单词书",
            Content = "四级大纲词汇",
            Color = "#866e66",
            NameSpace = ""
        };




        /// <summary>
        /// 生成任务栏
        /// </summary>
        void CreateTaskBar()
        {
            waitingToLearnTaskBar.Content = NumOfLearn.ToString();
            hasLearnedTaskBar.Content = NumOfHasLearn.ToString();
            waitingToReiviewTaskBar.Content = NumOfReview.ToString();

            TaskBars.Add(waitingToLearnTaskBar);
            TaskBars.Add(waitingToReiviewTaskBar);
            TaskBars.Add(hasLearnedTaskBar);
            TaskBars.Add(typeOfBookTaskBar);

        }

        /// <summary>
        /// 当已学习词书或者待复习词书发生变动时，更新任务栏
        /// </summary>
        private void UpdateTaskBar()
        {
            hasLearnedTaskBar.Content = NumOfHasLearn.ToString();

            TaskBars.Clear();
            CreateTaskBar();
        }


        private readonly IRegionManager regionManage;
        /// <summary>
        /// 任务栏导航委托
        /// </summary>
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="taskBar"></param>
        private void Navigate(TaskBar taskBar)
        {
            if (taskBar == null || string.IsNullOrWhiteSpace(taskBar.NameSpace))
            {
                return;
            }
            regionManage.Regions[PrismManager.MainViewRegionName].RequestNavigate(taskBar.NameSpace);
        }

        #endregion

        #region 每日文章
        //每日文章通知更新
        private ObservableCollection<DailyPage> dailyPages;
        public ObservableCollection<DailyPage> DailyPages
        {
            get { return dailyPages; }
            set { dailyPages = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 生成每日文章
        /// </summary>
        /*async*/
        async void  CreateDailyPage()
        {
             DailyPages.Add(new DailyPage
            {
                Image = "https://img2.chinadaily.com.cn/images/202303/10/640ae33ca31057c4b4b6d617.jpeg",
                Title = "Best spring destinations in Tibet",
                Content = "Known as \"the roof of the world\", the Qinghai-Tibet Plateau has long been an ideal and mysterious destination for visitors who are passionate about exploring remote lands. A high altitude plateau with mountains in all directions, a great range of cultural heritage sites, unique folk customs...all these factors contribute to the lure of Tibet. Now with the arrival of spring, the climate is becoming more pleasant and Tibet has become an even better tavel destination...",
                Flow = "https://www.chinadaily.com.cn/a/202303/10/WS640ae33ca31057c47ebb39f9.html"
            });


            ////调取API
            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage responseMessage = await client.GetAsync("http://api.mediastack.com/v1/news?access_key=1c9403cd7c4a8b2c8940ad941477fd69");
            //    if (responseMessage.IsSuccessStatusCode)
            //    {
            //        string responseContent = await responseMessage.Content.ReadAsStringAsync();
            //        //解析Json
            //        JObject jsonResponse = JObject.Parse(responseContent);
            //        JArray articles = (JArray)jsonResponse["data"];
            //        foreach (JToken article in articles)
            //        {
            //            if (articles.Count > 0)
            //            {
            //                string content = (string)article["description"];
            //                if (content.Length > 500)
            //                {
            //                    content = content.Substring(0, 500) + "...";
            //                }
            //                string image = (string)article["image"];
            //                if (image == null || image.Contains('%') || image.Contains('&') || image.Contains('$'))
            //                {
            //                    continue;
            //                }
            //                if (content.Length < 50)
            //                {
            //                    continue;
            //                }

            //                DailyPages.Add(new DailyPage
            //                {
            //                    Image = (string)article["image"],
            //                    Title = (string)article["title"],
            //                    Content = content,
            //                    Flow = (string)article["url"]

            //                });
            //                break;
            //            }
            //        }



            //    }
            //}

        }

        #endregion

        #region 单词查找

        private List<WordRoot.Root> words;
        /// <summary>
        /// 单词查找列表
        /// </summary>
        public List<WordRoot.Root> Words
        {
            get { return words; }
            set { words = value; RaisePropertyChanged(); }
        }

        private int selectedWordRank;
        /// <summary>
        /// 选中单词的序号
        /// </summary>
        public int SelectedWordRank
        {
            get { return selectedWordRank; }
            set { selectedWordRank = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(SelectedWord)); }
        }
        private WordRoot.Root selectedWord;
        /// <summary>
        /// 被选中的单词
        /// </summary>
        public WordRoot.Root SelectedWord
        {
            get
            {
                ShowTransCN();
                return Words[SelectedWordRank - 1];
            }
            set { selectedWord = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<SearchTransCN> transCNs;
        public ObservableCollection<SearchTransCN> TransCNs
        {
            get { return transCNs; }
            set { transCNs = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 显示释义
        /// </summary>
        public void ShowTransCN()
        {
            TransCNs.Clear();

            for (int i = 0; i < Words[SelectedWordRank - 1].content.word.content.trans.Count; i++)
            {
                SearchTransCN transCN = new SearchTransCN();
                transCN.PartOfSpeech = Words[SelectedWordRank - 1].content.word.content.trans[i].pos + ".";
                transCN.TransCN = Words[SelectedWordRank - 1].content.word.content.trans[i].tranCn;
                TransCNs.Add(transCN);


            }



        }

        /// <summary>
        /// 生词本命令
        /// </summary>
        public Command AddToNewWordBookCommand { get; set; }
        /// <summary>
        /// 生词本函数
        /// </summary>
        public void AddToNewWordBook()
        {
            //如果toggle被点击加入单词本
            if (Words[SelectedWordRank - 1].IsSettingNew == true)
            {

                //添加至生词本
                NewWordBook.NewWords.Add(SelectedWord);
            }
            else
            {

                //从单词本中移除
                NewWordBook.NewWords.Remove(SelectedWord);



            }
        }


        #endregion
    }
}

