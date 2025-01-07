namespace Talabat.APIs.DTOs.OrderDTOs
{
    public record OrderResponse
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public AddressRequest ShippingAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public decimal DeliveryCost { get; set; }
        public ICollection<OrderItemResponse> Items { get; set; } = new HashSet<OrderItemResponse>();
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
