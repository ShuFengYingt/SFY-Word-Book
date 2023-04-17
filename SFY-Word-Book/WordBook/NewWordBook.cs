using Prism.Mvvm;
using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.WordBook
{
    public class NewWordBook:BindableBase
    {
        public NewWordBook() {  }

        
        public static ObservableCollection<WordRoot.Root> NewWords { get; set; } = new ObservableCollection<WordRoot.Root>();

    }
}
