using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Common.Results
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T result)
        {
            Result = result;
        }
    }
}
