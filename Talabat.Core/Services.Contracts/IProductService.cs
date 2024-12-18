using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace Talabat.Core.Services.Contracts
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams specParams);
        Task<int> GetProductsCountAsync(ProductSpecParams specParams);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IReadOnlyList<Brand>> GetBrandsAsync();
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
    }
}
