using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Helpers.Filters
{
    public class CashedAttribute(int timeToLiveInSeconds) : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds = timeToLiveInSeconds;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // get the Cashing service with DI
            var responseCasheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            //check if response exists in cashe memory
            var casheKey = GenerateCasheKeyFromCurruntContext(context.HttpContext.Request);
            var cashedResponse = await responseCasheService.GetCachedResponse(casheKey);

            if (!string.IsNullOrWhiteSpace(cashedResponse))
            {
                var result = new ContentResult()
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = result;
                return;
            }

            var excutedActionContext = await next();

            if (excutedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value != null)
            {
                await responseCasheService.CachResponseAsync(casheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }

        }

        private string GenerateCasheKeyFromCurruntContext(HttpRequest request)
        {
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
