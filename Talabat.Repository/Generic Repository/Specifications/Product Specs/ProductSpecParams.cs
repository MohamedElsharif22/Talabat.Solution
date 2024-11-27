namespace Talabat.Repositories.Specifications.Product_Specs
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        private int pageSize = MaxPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set => pageSize = value > MaxPageSize || value <= 0 ? MaxPageSize : value;
        }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public bool GetCountOnly { get; set; }
    }
}
