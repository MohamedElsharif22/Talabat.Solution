using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.BasketDTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Repository.Contracts;

namespace Talabat.APIs.Controllers
{
    public class BasketController(IBasketRepository basketRepository, IMapper mapper) : BaseApiController
    {
        private readonly IBasketRepository _basketRepository = basketRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetCustomerBasket(string id = "")
        {

            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null
                ? Ok(new CustomerBasket() { Id = id })
                : Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basketDTO)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDTO);

            var createdOrUpdated = await _basketRepository.UpdateBasketAsync(basket);

            return createdOrUpdated is null
                ? BadRequest(new ApiResponse(400, "unable to create or update basket!"))
                : Ok(createdOrUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id)
                ? Ok(new ApiResponse(200, "Basket has been deleted succesfuly."))
                : BadRequest(new ApiResponse(400, "can't delete basket"));
        }
    }
}
