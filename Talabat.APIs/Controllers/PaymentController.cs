using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentController(IPaymentService paymentService) : BaseApiController
    {
        private readonly IPaymentService _paymentService = paymentService;
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string endpointSecret = "whsec_5e74ccbe521bb8e3c954abc50bffc8cc65b9ef1b3273d9e07d7ce1d3fa4701e5";

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [EndpointSummary("Create or Update PaymentIntent")]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Bad request, Unable to create or update payment intent for this basket!"));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);
                    break;
            }

            return Ok();

        }
    }


}

