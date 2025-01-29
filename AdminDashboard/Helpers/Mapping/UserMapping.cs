using AdminDashboard.Models.UserViewModels;
using Talabat.Core.Entities.Identity;

namespace AdminDashboard.Helpers.Mapping
{
    public static class UserMapping
    {
        public static UserResponseViewModel ToUserResponseViewModel(this ApplicationUser model, IList<string> roles)
        {
            return new UserResponseViewModel(roles)
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                UserName = model.UserName ?? string.Empty,
                Email = model.Email ?? "",
                PhoneNumber = model.PhoneNumber,
                Address = model.Address?.MapToAddressViewModel() ?? new AddressRequestViewModel(),

            };
        }
        public static UserRequestViewModel ToUserRequestViewModel(this ApplicationUser model, IEnumerable<UserInRoleViewModel> allRoles)
        {
            return new UserRequestViewModel()
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                UserName = model.UserName ?? string.Empty,
                Email = model.Email ?? string.Empty,
                Address = model.Address?.MapToAddressViewModel() ?? new AddressRequestViewModel(),
                PhoneNumber = model.PhoneNumber,
                Roles = allRoles

            };
        }

        public static ApplicationUser ToAppUser(this UserRequestViewModel model, ApplicationUser appUser)
        {

            appUser.UserName = model.UserName;
            appUser.Email = model.Email;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Address = model.Address.MapToUserAddress(appUser.Address);
            appUser.DisplayName = model.DisplayName;

            return appUser;

        }

        private static AddressRequestViewModel MapToAddressViewModel(this Address address)
        {
            return new AddressRequestViewModel()
            {
                City = address.City,
                Counrty = address.Counrty,
                Street = address.Street,

                Id = address.Id,
            };
        }

        private static Address MapToUserAddress(this AddressRequestViewModel addressViewModel, Address? userAddress)
        {
            if (userAddress is not null)
            {

                userAddress.City = addressViewModel.City;
                userAddress.Counrty = addressViewModel.Counrty;
                userAddress.Street = addressViewModel.Street;
                return userAddress;
            }
            return new Address
            {
                City = addressViewModel.City,
                Counrty = addressViewModel.Counrty,
                Street = addressViewModel.Street,
            };

        }
    }
}
