using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SFY_Word_Book.Common.Models
{
    public class WordStory:BindableBase
    {
        private string forUser;
        /// <summary>
        /// For+用户名
        /// </summary>
        public string ForUser { get;set; }

        private string title;
        /// <summary>
        /// 小故事标题
        /// </summary>
        public string Title { get; set; }

        private string story;
        /// <summary>
        /// 内容
        /// </summary>
        public string Story { get; set; }

        private string imageUrl;
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        private string date;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Date { get;set; }

        public bool IsCurrentStory { get; set; }

        public string StoryIndex { get; set; }


    }
}
