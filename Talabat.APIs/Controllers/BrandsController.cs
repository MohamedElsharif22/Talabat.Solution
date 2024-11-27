using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;

namespace Talabat.APIs.Controllers
{
    public class BrandsController(IGenericRepository<Brand> brandRepo, IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Brand> _brandRepo = brandRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandToReturnDto>>> GetAllBrands()
        {

            var brands = await _brandRepo.GetAllAsync();

            var brandsDto = brands.Select(b => _mapper.Map<BrandToReturnDto>(b));
            return Ok(brandsDto);
        }
    }
}
