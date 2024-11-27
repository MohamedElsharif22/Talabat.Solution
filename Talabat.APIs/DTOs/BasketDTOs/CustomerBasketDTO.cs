using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.BasketDTOs
{
    public class CustomerBasketDTO
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

        [Required]
        public List<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
    }
}
