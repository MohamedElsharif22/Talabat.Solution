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

        [EndpointSummary("Get customer basket")]
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id = "")
        {

            var basket = await _basketRepository.GetBasketAsync(id);
            basket ??= await _basketRepository.UpdateBasketAsync(new CustomerBasket() { Id = id });
            return Ok(basket);
        }

        [EndpointSummary("Update Basket")]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketRequest basketDTO)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDTO);
            var basketItems = basketDTO.BasketItems.Select(I => _mapper.Map<BasketItem>(I)).ToList();
            basket.BasketItems = basketItems;

            var createdOrUpdated = await _basketRepository.UpdateBasketAsync(basket);

            return createdOrUpdated is null
                ? BadRequest(new ApiResponse(400, "unable to create or update basket!"))
                : Ok(createdOrUpdated);
        }

        [EndpointSummary("Delete Basket")]
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id)
                ? Ok(new ApiResponse(200, "Basket has been deleted succesfuly."))
                : BadRequest(new ApiResponse(400, "can't delete basket"));
        }
    }
}
