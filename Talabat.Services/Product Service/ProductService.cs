using Talabat.Application.Specifications.Product_Specs;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace Talabat.Infrastructure.Product_Service
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<(IReadOnlyList<Product>, int)> GetAllProductsWithCountAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecification(specParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecsAsync(specs);
            var countSpecs = new ProductWithBrandAndCategorySpecification(specParams, getCountOnly: true);
            var count = await _unitOfWork.Repository<Product>().GetCountWithspecsAsync(countSpecs);
            return (products, count);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var specs = new ProductWithBrandAndCategorySpecification(P => P.Id == productId);

            return await _unitOfWork.Repository<Product>().GetWithSpecsAsync(specs);
        }


        public async Task<IReadOnlyList<Brand>> GetBrandsAsync()
        {
            return await _unitOfWork.Repository<Brand>().GetAllAsync();
        }


        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _unitOfWork.Repository<Category>().GetAllAsync();
        }

        public Task<int> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
