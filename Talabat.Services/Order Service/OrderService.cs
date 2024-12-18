﻿using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Services.Contracts;
using Talabat.Repositories.Generic_Repository.Specifications.Order_Specs;

namespace Talabat.Services.Order_Service
{
    public class OrderService(
        IBasketRepository basketRepo,
        IUnitOfWork unitOfWork) : IOrderService
    {
        private readonly IBasketRepository _basketRepo = basketRepo;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1.Get Basket From Baskets Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            if (basket?.BasketItems?.Count > 0)
            {
                var productRepo = _unitOfWork.Repository<Product>();
                foreach (var item in basket.BasketItems)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (product != null)
                    {
                        var productItemOrderd = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                        var orderItem = new OrderItem(productItemOrderd, product.Price, item.Quantity);

                        orderItems.Add(orderItem);
                    }
                }
            }
            else return null;
            // 3. Calculate SubTotal
            decimal subtotal = orderItems.Sum(I => I.Price * I.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            DeliveryMethod? deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 5. Create Order
            Order order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);

            _unitOfWork.Repository<Order>().Add(order);

            // 6. Save To Database [TODO]

            var result = await _unitOfWork.CompleteAsync();

            // 7. Empty Basket
            await _basketRepo.DeleteBasketAsync(basketId);

            return result <= 0 ? null : order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
            => await _unitOfWork.Repository<Order>().GetAllWithSpecsAsync(new OrderSpecifications(buyerEmail));

        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = await orderRepo.GetByIdWithSpecsAsync(spec);
            return order is null ? null : order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return deliveryMethods;
        }

    }
}