using System.Text.Json.Serialization;

namespace Talabat.APIs.DTOs.AccountDTOs
{
    public record AddressDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Counrty { get; set; } = null!;
        public string Street { get; set; } = null!;

    }
}
