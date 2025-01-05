using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Services.Contracts;
using Talabat.Repositories.Generic_Repository.Specifications.Order_Specs;

namespace Talabat.Services.Payment_Service
{
    public class PaymentService(
        IConfiguration configuration,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork) : IPaymentService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IBasketRepository _basketRepository = basketRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            PaymentIntent paymentIntent;
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            var shippingPrice = 0m;


            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return null;

            if (basket.BasketItems?.Count > 0)
            {
                var productRepo = _unitOfWork.Repository<Core.Entities.Product>();

                foreach (var item in basket.BasketItems)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (product is null) continue;

                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }
            else return null;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is not null)
                {
                    shippingPrice = deliveryMethod.Cost;
                    basket.ShippingPrice = shippingPrice;
                }
                else
                {
                    basket.DeliveryMethodId = null;
                    basket.ShippingPrice = 0;
                }

            }

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = basket.BasketItems.Sum(P => (long)(P.Price * 100) * P.Quantity) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update PaymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = basket.BasketItems.Sum(P => (long)(P.Price * 100) * P.Quantity) + (long)shippingPrice * 100,
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var order = await orderRepo.GetWithSpecsAsync(new OrderSpecifications(O => O.PaymentIntentId == paymentIntentId));

            if (order is null)
                return null;

            if (isPaid)
                order.Status = OrderStatus.PaymentReceived;
            else
                order.Status = OrderStatus.PaymentFailed;

            await _unitOfWork.CompleteAsync();

            return order;

        }
    }
}
