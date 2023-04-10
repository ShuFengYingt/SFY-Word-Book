using Prism.Mvvm;
using SFY_Word_Book.Common.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SFY_Word_Book.Common.Models
{
    public class DailyPage : BindableBase
    {
        public DailyPage()
        {
            ShowPageFlowCommand = new Command(ShowPageFlow);
        }
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

        public Command ShowPageFlowCommand { get; set; }


        public void ShowPageFlow()
        {
            var flow = Flow;
            var sInfo = new ProcessStartInfo(flow)
            {
                UseShellExecute = true,
            };
            Process.Start(sInfo);
        }

    }
}
