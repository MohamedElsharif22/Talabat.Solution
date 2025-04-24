using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contracts;
using Talabat.Application._Data;
using Talabat.Application._Identity;
using Talabat.Infrastructure.Product_Service;
using Talabat.Application;

namespace AdminDashboard.MVC.Extentions
{
    public static class ApplicationServicesExtentions
    {
        #region Databases Configrations
        public static IServiceCollection AddDatabasesServices(this IServiceCollection services, IConfiguration _configuration)
        {
            // Configure store DbContexts with DI
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("MainConnectionString"));
            });

            // Add Identity Service To Container DI
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
        #endregion
        #region Authentication Configrations
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration _configuration)
        {

            //Add Identity Services to DI Container
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();


            return services;
        }
        #endregion


        #region Application Configrations
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Product Service DI
            services.AddScoped<IProductService, ProductService>();


            return services;
        }
        #endregion
    }
}
