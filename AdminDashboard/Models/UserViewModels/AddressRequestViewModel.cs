namespace AdminDashboard.Models.UserViewModels
{
    public class AddressRequestViewModel
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string Counrty { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
}
