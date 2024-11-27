namespace Talabat.Core.Entities.Basket
{
    public class CustomerBasket
    {
        private Guid id = Guid.NewGuid();

        public string Id
        {
            get { return id.ToString(); }
            set
            {
                var flag = Guid.TryParse(value, result: out _);
                if (flag)
                    id = Guid.Parse(value);
                else
                    id = Guid.NewGuid();
            }
        }
        public List<BasketItem> BasketItems { get; set; } = [];
    }
}
