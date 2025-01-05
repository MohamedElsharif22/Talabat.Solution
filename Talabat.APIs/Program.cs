using System.Text.Json.Serialization;
using Talabat.APIs.Extentions;
using Talabat.APIs.Middlewares;

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
                .AddJsonOptions(configure =>
                {
                    configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();


            // Add DBs Services
            builder.Services.AddDatabasesServices(builder.Configuration);

            // Adding Application Required services with Extention method
            builder.Services.AddApplicationServices();

            // Authentication Services
            builder.Services.AddAuthenticationServices(builder.Configuration);

            // add cors policies
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("wepPolicy", policyConfig =>
                {
                    policyConfig.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });

            });

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

            app.UseCors("wepPolicy");

            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();


            #endregion

            app.Run();
        }
    }
}
