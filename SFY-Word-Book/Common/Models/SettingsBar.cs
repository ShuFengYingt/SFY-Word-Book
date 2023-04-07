using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common.Models
{
    public class SettingsBar
    {
        private string icon;
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get { return icon; } set { icon = value; } }

        private string title;
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get { return title; } set { title = value; } }

        private string nameSpace;
        /// <summary>
        /// 操作命名
        /// </summary>
        public string NameSpace { get {  return nameSpace; } set {  nameSpace = value; } }
    }
}
