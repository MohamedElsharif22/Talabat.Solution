namespace Talabat.APIs.Helpers
{
    public class Pagination<T>(int pageIndex, int pageSize, int count, IEnumerable<T> data)
    {
        public int PageSize { get; set; } = pageSize;
        public int PageIndex { get; set; } = pageIndex;
        public int Count { get; set; } = count;
        public IEnumerable<T> Data { get; set; } = data;
    }
}
