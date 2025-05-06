using AdminDashboard.MVC.Models;
using Talabat.Core.Entities;

namespace AdminDashboard.MVC.Helpers.Mapping
{
    public static class CategoryMapping
    {
        public static CategoryViewModel ToViewModel(this Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public static Category ToEntity(this CategoryViewModel categoryViewModel)
        {
            return new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }
    }
}
