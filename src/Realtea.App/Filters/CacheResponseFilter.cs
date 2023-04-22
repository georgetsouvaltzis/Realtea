using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Realtea.App.Cache;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Realtea.App.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheResponseAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachingService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            string userId = string.Empty;
            var authenticateResult = await context.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (authenticateResult.Succeeded)
                userId = authenticateResult.Principal.FindFirstValue("sub");

            var generatedKey = GenerateCacheKey(context.HttpContext.Request, userId);

            var cachedResponse = cachingService.Get(generatedKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var response = new ContentResult
                {
                    Content = cachedResponse,
                    StatusCode = (int)HttpStatusCode.OK,
                    ContentType = "application/json",
                };

                context.Result = response;
                return;
            }

            var nextResult = await next();

            if (nextResult.Result is OkObjectResult ok)
            {
                cachingService.Set(generatedKey, ok.Value);
            }
        }


        private string GenerateCacheKey(HttpRequest request, string userId)
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(userId))
                stringBuilder.Append($"{userId}-");

            stringBuilder.Append($"{request.Path}");

            foreach (var query in request.Query)
                stringBuilder.Append($"{query.Key}={query.Value}");

            return stringBuilder.ToString();
        }
    }
}
