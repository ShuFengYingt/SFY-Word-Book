using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    /// <summary>
    /// 任务栏
    /// </summary>
    public class TaskBars:BindableBase
    {
        /// <summary>
        /// 任务栏构造函数
        /// </summary>
        public TaskBars()
        {
        
        }

        //图标
        private string icon;
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        //标题
        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get;set; }

        //数据信息
        private string content;
        /// <summary>
        /// 数据信息
        /// </summary>
        public string Content { get { return content; } set { content = value; } }

        //颜色
        private string color;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public string Color { get { return color; } set { color = value; } }

        //用于触发事件的命名空间
        private string nameSpace;
        /// <summary>
        /// 用于触发事件的命名空间
        /// </summary>
        public string NameSpace { get {  return nameSpace; } set {  nameSpace = value; } }

    }

}
