namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Address
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
