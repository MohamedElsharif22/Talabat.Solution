using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace AdminDashboard.MVC.Controllers
{
    public class ProductsController(IProductService productService, IMapper mapper) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index(ProductSpecParams productSpec)
        {

            var products = await _productService.GetAllProductsAsync(productSpec);

            var mappedProducts = products.Select(p => _mapper.Map<ProductResponse>(p));

            return View(mappedProducts);
        }
    }
}
