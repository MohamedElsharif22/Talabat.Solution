using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat.APIs.Extentions
{
    public static class ManageDatabaseExtention
    {

        public static async Task<WebApplication> MiagrateAndSeedDatabasesAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _storeDbContext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _storeDbContext.Database.MigrateAsync();
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
