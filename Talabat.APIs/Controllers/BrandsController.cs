using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core;
using Talabat.Core.Entities;

namespace Talabat.APIs.Controllers
{
    public class BrandsController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandResponse>>> GetAllBrands()
        {

            var brands = await _unitOfWork.Repository<Brand>().GetAllAsync();

            var brandsDto = brands.Select(b => _mapper.Map<BrandResponse>(b));
            return Ok(brandsDto);
        }
    }
}
