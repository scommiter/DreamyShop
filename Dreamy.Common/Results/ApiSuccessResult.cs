namespace Dreamy.Common.Results
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T result)
        {
            Result = result;
        }
    }
}
