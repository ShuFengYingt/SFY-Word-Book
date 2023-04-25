using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Shared.Parameter
{
    /// <summary>
    /// 分页查询
    /// </summary>
    public class QueryParameter
    {
        public int PageIndex { get;set; }
        public int PageSize { get;set; }
        public string Search { get; set; }
    }
}
