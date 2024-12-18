using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Specifications;

namespace Talabat.Repositories.Generic_Repository.Specifications.Order_Specs
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail)
        {
            AddOrderIncludes();

            AddOrderByDesc(O => O.OrderDate);
        }
        public OrderSpecifications(string buyerEmail, int orderId)
            : base(O => O.BuyerEmail == buyerEmail && O.Id == orderId)
        {
            AddOrderIncludes();
        }

        private void AddOrderIncludes()
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
