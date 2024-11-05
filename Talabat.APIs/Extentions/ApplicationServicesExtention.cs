using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repository.Contracts;
using Talabat.Repository.Repository;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // add AutoMapper To DI Scope
            //services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()), Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            // Handling validation errors response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value?.Errors.Count > 0)
                                                         .SelectMany(P => P.Value.Errors)
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
    }
}
