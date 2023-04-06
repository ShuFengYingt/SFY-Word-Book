using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SFY_Word_Book.Common.Models
{
    public class DailyPage:BindableBase
    {
        //文章封面
        private string image;
        public string Image
        { 
            get { return image; } 
            set { image = value; RaisePropertyChanged(); }
        }

        //文章标题
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged();}
        }

        //文章内容
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged();}
        }

        //文章源
        private string flow;
        public string Flow
        {
            get { return flow; }
            set { flow = value; RaisePropertyChanged();}
        }
    }
}
