using Microsoft.VisualBasic;
using Prism.Mvvm;
using SFY_Word_Book.Common.Models;
using SFY_Word_Book.Common.Modles;
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

namespace SFY_Word_Book.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel()
        {
            //初始化
            TaskBars = new ObservableCollection<TaskBars>();
            dailyPages = new ObservableCollection<DailyPage>();

            CreateTaskBar();
            CreateDailyPage();
        }

        //任务栏的通知更新
        private ObservableCollection<TaskBars> taskBars;
        public ObservableCollection<TaskBars> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        void CreateTaskBar()
        {

            TaskBars.Add(new TaskBars { Icon = "BookOpenPageVariant", Title = "待学习", Content = "2677", Color = "#48815a", NameSpace = "" });
            TaskBars.Add(new TaskBars { Icon = "BookClock", Title = "待复习", Content = "0", Color = "#008184", NameSpace = "" });
            TaskBars.Add(new TaskBars { Icon = "BookCheck", Title = "已学习", Content = "121", Color = "#965fa0", NameSpace = "" });
            TaskBars.Add(new TaskBars { Icon = "Bookshelf", Title = "单词书", Content = "四级大纲词汇", Color = "#866e66", NameSpace = "" });

        }


        //每日文章通知更新
        private ObservableCollection<DailyPage> dailyPages;
        public ObservableCollection<DailyPage> DailyPages
        {
            get { return dailyPages; }
            set { dailyPages = value; RaisePropertyChanged(); }
        }

        async void CreateDailyPage()
        {
            //调取API
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("http://api.mediastack.com/v1/news?access_key=1c9403cd7c4a8b2c8940ad941477fd69");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await responseMessage.Content.ReadAsStringAsync();
                    //解析Json
                    JObject jsonResponse = JObject.Parse(responseContent);
                    JArray articles = (JArray)jsonResponse["data"];
                    foreach (JToken article in articles)
                    {
                        if (articles.Count > 0)
                        {
                            string content = (string)article["description"];
                            if (content.Length > 500)
                            {
                                content = content.Substring(0, 500) + "...";
                            }
                            string image = (string)article["image"];
                            if (image == null || image.Contains('%') || image.Contains('&') || image.Contains('$'))
                            {
                                continue;
                            }
                            if (content.Length < 50)
                            {
                                continue;
                            }

                            DailyPages.Add(new DailyPage
                            {
                                Image = (string)article["image"],
                                Title = (string)article["title"],
                                Content = content,
                                Flow = (string)article["url"]
                                
                            });
                            break;
                        }
                    }

                    

                }
            }

        }


    }
}
