using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Services.Contracts
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);
    }
}
