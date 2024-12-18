using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specifications.Product_Specification_Params;
using Talabat.Repositories.Specifications.Product_Specs;

namespace Talabat.Services.Product_Service
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecification(specParams);
            return await _unitOfWork.Repository<Product>().GetAllWithSpecsAsync(specs);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var specs = new ProductWithBrandAndCategorySpecification(P => P.Id == productId);

            return await _unitOfWork.Repository<Product>().GetByIdWithSpecsAsync(specs);
        }

        public async Task<int> GetProductsCountAsync(ProductSpecParams specParams)
        {
            var countSpec = new ProductWithBrandAndCategorySpecification(specParams: specParams, getCountOnly: true);

            return await _unitOfWork.Repository<Product>().GetCountWithspecsAsync(countSpec);
        }

        public Task<IReadOnlyList<Brand>> GetBrandsAsync()
        {
            throw new NotImplementedException();
        }


        public Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
