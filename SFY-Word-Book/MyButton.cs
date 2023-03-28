using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SFY_Word_Book
{
    class MyButton : Button
    {
        public Type UserWindowType { get; set; }

        //重写按键触发
        protected override void OnClick()
        {
            base.OnClick();//当基类触发该事件
            Window win = Activator.CreateInstance(this.UserWindowType) as Window; //获取用户按钮类型
            
            //default
            if (win != null)
            {
                win.ShowDialog();
            }
        }
    }
}
