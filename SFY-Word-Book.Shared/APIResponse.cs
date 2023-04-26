using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Shared
{
    public class APIResponse
    {
        public string Message { get; set; }

        public bool Statue { get; set; }

        public object Result { get; set; }


    }
    public class APIResponse<T>
    {
        public string Message { get; set; }

        public bool Statue { get; set; }

        public T Result { get; set; }
    }

}
