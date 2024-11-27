using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.Repositories._Identity;
using Talabat.Repositories.Generic_Repository.Data;

namespace Talabat.APIs.Extentions
{
    public static class ManageDatabasesExtention
    {

        public static async Task<WebApplication> MiagrateAndSeedDatabasesAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _storeDbContext = services.GetRequiredService<StoreContext>();

            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _storeDbContext.Database.MigrateAsync();
                await _identityDbContext.Database.MigrateAsync();
                var _userManeger = services.GetRequiredService<UserManager<ApplicationUser>>();
                await IdentityDbContextSeed.SeedDataAsync(_userManeger);
                await StoreDbContextSeed.SeedAsync(_storeDbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "an error has been occured while running migration!");
            }

            return webApplication;
        }
    }
}
