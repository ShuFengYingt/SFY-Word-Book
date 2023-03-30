using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book
{
    /// <summary>
    /// 通知更新封装类
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 传入属性名称，实现value更新
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")//CallerMemberName自动获取调用方法或者属性的名称并为该参数赋值，
                                                                                 //需要设置defualt，这里以""为default
                                                                                 //这样调用的时候就不需要传入属性的名字了
                                                                                 //非常优雅的方法
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
