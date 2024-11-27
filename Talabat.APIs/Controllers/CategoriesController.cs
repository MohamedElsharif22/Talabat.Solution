using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;

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
            var cats = await _categoryRepo.GetAllAsync();

            var catsDto = cats.Select(C => _mapper.Map<CategoryToReturnDTO>(C));

            return Ok(catsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDTO>>> GetCategoryById(int id)
        {
            var cat = await _categoryRepo.GetByIdAsync(id);

            var catDto = _mapper.Map<CategoryToReturnDTO>(cat);

            return Ok(catDto);
        }

        //[HttpGet("Products/{id}")]
        //public Task<ActionResult<IReadOnlyList<ProductToGetDto>>> GetRelatedProducts(int id)
        //{

        //}



    }
}
