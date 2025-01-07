using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.OrderDTOs
{
    public record OrderRequest
    {
        // string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }

        public AddressRequest shippingAddress { get; set; }
    }
}
