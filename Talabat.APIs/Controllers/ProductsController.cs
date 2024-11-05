using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repository.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController(IGenericRepository<Product> productRepository,
                                    IGenericRepository<Brand> brandRepo,
                                    IGenericRepository<Category> categoryRepo
                                    , IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IGenericRepository<Brand> _brandRepo = brandRepo;
        private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;
        private readonly IMapper _mapper = mapper;

        [ProducesResponseType(typeof(ProductToGetDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToGetDto>>> GetAllProducts()
        {
            var specs = new ProductWithBrandAndCategorySpecification();
            var products = await _productRepository.GetAllWithSpecsAsync(specs);

            var productsDto = products.Select(P => _mapper.Map<ProductToGetDto>(P));

            return Ok(productsDto);
        }


        [ProducesResponseType(typeof(ProductToGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToGetDto>> GetProductById(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecification(P => P.Id == id);

            var product = await _productRepository.GetByIdWithSpecsAsync(specs);
            if (product is null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<ProductToGetDto>(product));

        }






    }
}
