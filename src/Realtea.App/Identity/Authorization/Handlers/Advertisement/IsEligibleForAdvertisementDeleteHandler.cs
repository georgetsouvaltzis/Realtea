using Microsoft.AspNetCore.Authorization;
using Realtea.App.Identity.Authorization.Requirements.Advertisement;
using Realtea.Core.Results.Advertisement;
using System.Security.Claims;

namespace Realtea.App.Identity.Authorization.Handlers.Advertisement
{
    public class IsEligibleForAdvertisementDeleteHandler : AuthorizationHandler<IsEligibleForAdvertisementDeleteRequirement, AdvertisementResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEligibleForAdvertisementDeleteRequirement requirement, AdvertisementResult resource)
        {
            var userId = Convert.ToInt32(context.User.FindFirstValue("sub"));

            if (resource.UserId == userId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}