using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repository.Specifications.Brand_Specs;

namespace Talabat.APIs.Controllers
{
    public class BrandsController(IGenericRepository<Brand> brandRepo, IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Brand> _brandRepo = brandRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandToReturnDto>>> GetAllBrands()
        {
            var specs = new BrandWithRelatedProductsSpecs();
            var brands = await _brandRepo.GetAllWithSpecsAsync(specs);
            var brandsDto = brands.Select(b => _mapper.Map<BrandToReturnDto>(b));
            return Ok(brandsDto);
        }
    }
}
