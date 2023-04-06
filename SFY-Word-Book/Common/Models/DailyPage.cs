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
        /// <summary>
        /// 文章封面
        /// </summary>
        private string image;
        public string Image
        { 
            get { return image; } 
            set { image = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 文章标题
        /// </summary>
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged();}
        }

        /// <summary>
        /// 文章内容
        /// </summary>
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged();}
        }

        /// <summary>
        /// 文章源
        /// </summary>
        private string flow;
        public string Flow
        {
            get { return flow; }
            set { flow = value; RaisePropertyChanged();}
        }
    }
}
