using DreamyShop.Common.Exceptions;

namespace DreamyShop.Common.Results
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(int errorCode)
        {
            Code = errorCode;
            Message = Enum.GetName(typeof(ErrorCodes), errorCode)!;
        }
    }
}