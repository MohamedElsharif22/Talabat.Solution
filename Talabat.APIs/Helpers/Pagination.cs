namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, IEnumerable<T> productsDto)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = productsDto;
            Count = count;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
