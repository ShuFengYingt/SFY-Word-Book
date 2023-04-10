using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFY_Word_Book.Common.Commands
{
    public class Command : ICommand
    {
        public Command(Action action)
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
