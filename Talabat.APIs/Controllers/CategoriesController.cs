using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repository.Specifications.Category_Specs;

namespace Talabat.APIs.Controllers
{
    public class CategoriesController(IGenericRepository<Category> categoryRepo, IGenericRepository<Product> productRepo, IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepo = categoryRepo;
        private readonly IGenericRepository<Product> _productRepo = productRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDTO>>> GetAllCategories()
        {
            var sepcs = new CategoryWithRelatedProductsSpecifications();
            var cats = await _categoryRepo.GetAllWithSpecsAsync(sepcs);

            var catsDto = cats.Select(C => _mapper.Map<CategoryToReturnDTO>(C));

            return Ok(catsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDTO>>> GetCategoryById(int id)
        {
            var sepcs = new CategoryWithRelatedProductsSpecifications(C => C.Id == id);
            var cat = await _categoryRepo.GetByIdWithSpecsAsync(sepcs);

            var catDto = _mapper.Map<CategoryToReturnDTO>(cat);

            return Ok(catDto);
        }

        [HttpGet("Products/{id}")]
        public Task<ActionResult<IReadOnlyList<ProductToGetDto>>> GetRelatedProducts(int id)
        {

        }



    }
}
