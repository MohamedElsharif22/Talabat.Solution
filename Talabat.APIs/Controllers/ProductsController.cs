using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Helpers.Filters;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace Talabat.APIs.Controllers
{
    public class ProductsController(IProductService productService
                                    , IMapper mapper) : BaseApiController
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        [EndpointSummary("Get all products")]
        [Cashed(600)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var (products, count) = await _productService.GetAllProductsWithCountAsync(specParams);

            if (products == null) return NoContent();

            var productsResponse = products.Select(P => _mapper.Map<ProductResponse>(P));

            var page = new Pagination<ProductResponse>(specParams.PageIndex, specParams.PageSize, count, productsResponse);

            return Ok(page);
        }


        [Cashed(600)]
        [EndpointSummary("Get product by id")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<ProductResponse>(product));

        }




    }
}
