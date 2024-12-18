using Microsoft.AspNetCore.Identity;
using Talabat.APIs.DTOs.AccountDTOs;
using Talabat.APIs.DTOs.OrderDTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Extentions
{
    public static class MappingExtentions
    {
        public static async Task<UserResponse> ToUserResponseAsync(this ApplicationUser user, IAuthService _authService, UserManager<ApplicationUser> _userManager)
        {
            return new UserResponse
            {
                Name = user.DisplayName,
                Email = user.Email!,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            };
        }

        public static OrderResponse ToOrderResponse(this Order order, IConfiguration configuration)
        {
            return new OrderResponse()
            {
                Id = order.Id,
                ShippingAddress = new AddressRequest()
                {
                    FirstName = order.ShippingAddress.FirstName,
                    LastName = order.ShippingAddress.LastName,
                    Street = order.ShippingAddress.Street,
                    City = order.ShippingAddress.City,
                    Country = order.ShippingAddress.Country,
                },
                DeliveryMethod = order.DeliveryMethod.ShortName,
                DeliveryCost = order.DeliveryMethod.Cost,
                DeliveryTime = order.DeliveryMethod.DeliveryTime,
                OrderDate = order.OrderDate,
                BuyerEmail = order.BuyerEmail,
                Status = order.Status.ToString(),
                Subtotal = order.Subtotal,
                Total = order.Total,
                Items = order.Items.Select(I => new OrderItemResponse()
                {
                    Id = I.Id,
                    ProductId = I.Product.ProductId,
                    PictureUrl = !string.IsNullOrWhiteSpace(I.Product.PictureUrl) ? $"{configuration["BaseApiUrl"]}/{I.Product.PictureUrl}" : string.Empty,
                    ProductName = I.Product.ProductName,
                    Price = I.Price,
                    Quantity = I.Quantity,
                }).ToHashSet(),
                PaymentIntentId = order.PaymentIntentId,
            };
        }
    }
}
