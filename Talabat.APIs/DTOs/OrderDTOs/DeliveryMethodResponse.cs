namespace Talabat.APIs.DTOs.OrderDTOs
{
    public class DeliveryMethodResponse
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; } = null!;
    }
}
