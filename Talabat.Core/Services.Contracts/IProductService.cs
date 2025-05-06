using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace Talabat.Core.Services.Contracts
{
    public interface IProductService
    {
        Task<(IReadOnlyList<Product>, int)> GetAllProductsWithCountAsync(ProductSpecParams specParams);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IReadOnlyList<Brand>> GetBrandsAsync();
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<int> UpdateProduct(Product product);
        Task<bool> AddProduct(Product product);
        Task<bool> DeleteProduct(int productId);
    }
}
