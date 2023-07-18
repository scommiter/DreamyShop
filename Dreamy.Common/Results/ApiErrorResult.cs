using Dreamy.Common.Exceptions;

namespace Dreamy.Common.Results
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
