namespace Dreamy.Common.Results
{
    public class PageResult<T> where T : class
    {
        public int Totals { get; set; }
        public List<T> Items { get; set; }
    }
}
