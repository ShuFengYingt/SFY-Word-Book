using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFY_Word_Book
{
    public class MyCommand : ICommand
    {
        //创建一个委托
        Action executeAction;

        //传入外部函数至此委托
        public MyCommand(Action action)
        {
            this.executeAction = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //执行委托的函数
            executeAction();
        }
    }
}
