using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Services;
using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;

namespace Realtea.App.Authorization
{

    public class IsEligibleForAdvertisementDeleteRequirement : IAuthorizationRequirement
    {

    }

    public class IsEligibleForAdvertisementUpdateRequirement : IAuthorizationRequirement
    {

    }


    public class IsEligibleForAdvertisementUpdateAuthHandler : AuthorizationHandler<IsEligibleForAdvertisementUpdateRequirement, ReadAdvertisementDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEligibleForAdvertisementUpdateRequirement requirement, ReadAdvertisementDto resource)
        {
            var userId = Convert.ToInt32(context.User.FindFirstValue("sub"));

            if(resource.UserId == userId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

    public class IsEligibleForAdvertisementDeleteAuthHandler : AuthorizationHandler<IsEligibleForAdvertisementDeleteRequirement, ReadAdvertisementDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsEligibleForAdvertisementDeleteRequirement requirement, ReadAdvertisementDto resource)
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

