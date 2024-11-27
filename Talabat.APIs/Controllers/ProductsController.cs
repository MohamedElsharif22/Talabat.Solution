using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repositories.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
    public class ProductsController(IGenericRepository<Product> productRepository
                                    , IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [ProducesResponseType(typeof(ProductToGetDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToGetDto>>> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecification(specParams);
            var products = await _productRepository.GetAllWithSpecsAsync(specs);

            var productsDto = products.Select(P => _mapper.Map<ProductToGetDto>(P));

            // applying specs to get the count After filteration
            specParams.GetCountOnly = true;
            var countSpec = new ProductWithBrandAndCategorySpecification(specParams);

            int count = await _productRepository.GetCountWithspecsAsync(countSpec);

            var page = new Pagination<ProductToGetDto>(specParams.PageIndex, specParams.PageSize, count, productsDto);

            return Ok(page);
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
