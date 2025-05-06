using AdminDashboard.MVC.Helpers.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.APIs.Helpers;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specifications.Product_Specification_Params;

namespace AdminDashboard.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public sealed class ProductsController(IProductService productService, IConfiguration configuration) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IActionResult> Index(ProductSpecParams productSpec)
        {

            var client = new HttpClient();

            var response = await client.GetAsync($"{_configuration["BaseApiUrl"]}/api/Products?brandId={productSpec.BrandId}&categoryId={productSpec.CategoryId}&sort={productSpec.Sort}&pageSize={productSpec.PageSize}&pageIndex={productSpec.PageIndex}&search={productSpec.Search}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = await JsonSerializer.DeserializeAsync<Pagination<ProductResponse>>(new MemoryStream(Encoding.UTF8.GetBytes(content)),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });

                var brands = await _productService.GetBrandsAsync();
                var categories = await _productService.GetCategoriesAsync();

                ViewBag.CurrentBrandId = productSpec.BrandId;
                ViewBag.CurrentCategoryId = productSpec.CategoryId;
                ViewBag.CurrentSort = productSpec.Sort;
                ViewBag.CurrentSearch = productSpec.Search;

                return View((products, brands.Select(b => b.ToViewModel()), categories.Select(c => c.ToViewModel())));
            }
            else
            {
                // Handle error response
                ModelState.AddModelError(string.Empty, "Unable to load products.");
                return View();
            }
        }

        // For Ajax requests
        [HttpGet]
        public async Task<IActionResult> GetProductsPartial(ProductSpecParams productSpec)
        {
            // Ensure defaults for pagination
            if (productSpec.PageIndex <= 0) productSpec.PageIndex = 1;
            if (productSpec.PageSize <= 0) productSpec.PageSize = 10; // Default page size

            var client = new HttpClient();
            // Build the query string
            var queryString = $"brandId={productSpec.BrandId}&categoryId={productSpec.CategoryId}" +
                              $"&sort={productSpec.Sort ?? ""}&pageSize={productSpec.PageSize}" +
                              $"&pageIndex={productSpec.PageIndex}&search={productSpec.Search ?? ""}";

            var response = await client.GetAsync($"{_configuration["BaseApiUrl"]}/api/Products?{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = await JsonSerializer.DeserializeAsync<Pagination<ProductResponse>>(
                    new MemoryStream(Encoding.UTF8.GetBytes(content)),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return PartialView("Products_Partial/_ProductsPartial", products);
            }
            else
            {
                // Return an error partial
                return PartialView("_ErrorPartial", "Unable to load products.");
            }
        }
    }
}
