using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.Repositories._Data;
using Talabat.Repositories._Identity;

namespace AdminDashboard.Extentions
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
    }
}
