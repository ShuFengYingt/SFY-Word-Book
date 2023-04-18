using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SFY_Word_Book.WordBook
{
     public class ReviewWordBook
     {
        public ReviewWordBook()
        {

        }

        public static ObservableCollection<WordRoot.Root> ReviewWords { get; set; } = new ObservableCollection<WordRoot.Root>();


     }
}
