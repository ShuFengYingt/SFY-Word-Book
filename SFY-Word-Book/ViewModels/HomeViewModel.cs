using Prism.Mvvm;
using SFY_Word_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.ViewModels
{
    public class HomeViewModel:BindableBase
    {
        public HomeViewModel() 
        {
            //初始化
            TaskBars = new ObservableCollection<TaskBars>();
            CreateTaskBar();
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

    }
}
    