using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFY_Word_Book.Common.Command
{
    public class HomeViewCommand : ICommand
    {
        public HomeViewCommand(Action action)
        {
            excuteAction = action;
        }
        Action excuteAction;
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            excuteAction();
        }
    }
}
