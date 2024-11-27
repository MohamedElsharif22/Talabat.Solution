using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Services.Contracts;
using Talabat.Repositories._Identity;
using Talabat.Repositories.Basket_Repository;
using Talabat.Repositories.Generic_Repository;
using Talabat.Repositories.Generic_Repository.Data;
using Talabat.Services.Auth_Service;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {

        #region Databases Configrations
        public static IServiceCollection AddDatabasesServices(this IServiceCollection services, IConfiguration _configuration)
        {
            // Configure store DbContexts with DI
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("MainConnectionString"));
            });

            // Configure Redis Database DI
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("redis")!);
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
            // Adding Authintication Schema Bearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["JWT:ValidAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            return services;
        }
        #endregion

        #region Application Configrations
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // add AutoMapper To DI Scope
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // Add Basket Service To the Scope
            services.AddSingleton<IBasketRepository, BasketRepository>();

            //Add Identity Services to DI Container
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            // Apply DI For AuthService
            services.AddScoped<IAuthService, AuthService>();

            // Handling validation errors response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value?.Errors.Count > 0)
                                                         .SelectMany(P => P.Value!.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
        #endregion
    }
}
