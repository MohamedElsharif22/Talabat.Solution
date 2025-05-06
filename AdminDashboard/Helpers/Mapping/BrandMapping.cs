using AdminDashboard.MVC.Models;
using Talabat.Core.Entities;

namespace AdminDashboard.MVC.Helpers.Mapping
{
    public static class BrandMapping
    {
        public static BrandViewModel ToViewModel(this Brand brand)
        {
            return new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
        public static Brand ToEntity(this BrandViewModel brandViewModel)
        {
            return new Brand
            {
                Id = brandViewModel.Id,
                Name = brandViewModel.Name
            };
        }

    }
}
