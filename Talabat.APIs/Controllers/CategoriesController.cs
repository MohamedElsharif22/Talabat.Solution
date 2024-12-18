﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core;
using Talabat.Core.Entities;

namespace Talabat.APIs.Controllers
{
    public class CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> GetAllCategories()
        {
            var cats = await _unitOfWork.Repository<Category>().GetAllAsync();

            var catsDto = cats.Select(C => _mapper.Map<CategoryResponse>(C));

            return Ok(catsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> GetCategoryById(int id)
        {
            var cat = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

            var catDto = _mapper.Map<CategoryResponse>(cat);

            return Ok(catDto);
        }


    }
}
