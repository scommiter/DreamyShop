namespace Dreamy.Common.Results
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
