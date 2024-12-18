using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs.OrderDTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController(IOrderService orderService, IMapper mapper, IConfiguration configuration) : BaseApiController
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;

        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("Create New Order")]
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
        {
            Address address = _mapper.Map<Address>(orderRequest.shippingAddress);
            Order? order = await _orderService.CreateOrderAsync(orderRequest.BuyerEmail, orderRequest.BasketId, orderRequest.DeliveryMethodId, address);

            if (order is null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Bad request, unable to create order!"));

            return Ok(order.ToOrderResponse(_configuration));
        }

        [EndpointDescription("Get All Orders For Specific user")]
        [ProducesResponseType(typeof(IReadOnlyList<OrderResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetUserOrders(string email)
        {
            var orders = await _orderService.GetOrdersForUserAsync(email);

            if (orders is null) return NoContent();

            return Ok(orders.Select(O => O.ToOrderResponse(_configuration)));
        }

        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(int id, string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(email, id);

            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(order.ToOrderResponse(_configuration));
        }

        [ProducesResponseType(typeof(IEnumerable<DeliveryMethodResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            if (deliveryMethods is null) return NoContent();


            return Ok(deliveryMethods.Select(M => _mapper.Map<DeliveryMethodResponse>(M)));
        }
    }
}
