using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Talabat.APIs.Extentions;
using Talabat.APIs.Middlewares;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Services Configurations

            builder.Services.AddControllers()
                             .AddJsonOptions(config =>
                             {
                                 config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                             });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            // Configure store DbContexts with DI
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectionString"));
            });


            // Adding Application Required services with Extention method
            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();

            // Applying Migrations Before Running app with  Extention Method
            await app.MiagrateAndSeedDatabasesAsync();

            #region configure Middlewares
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
