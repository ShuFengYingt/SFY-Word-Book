using SFY_Word_Book.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.WordBook
{
     public class ReviewWordBook
     {
        public ReviewWordBook()
        {
        }

        public static List<WordRoot.Root> ReviewWords { get; set; } = new List<WordRoot.Root>();

     }
}
