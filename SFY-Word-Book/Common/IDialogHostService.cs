using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Common
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
