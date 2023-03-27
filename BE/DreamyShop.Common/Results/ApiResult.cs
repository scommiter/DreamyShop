using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Common.Results
{
    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class PageResult<T> where T : class
    {
        public int Totals { get; set; }
        public List<T> Items { get; set; }
    }
}
