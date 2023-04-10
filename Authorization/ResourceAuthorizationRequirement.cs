//namespace IdealDiscuss.Authorization;

//using System.Diagnostics;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using IdealDiscuss.Context;
//using IdealDiscuss.Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.EntityFrameworkCore;

//public class ResourceAuthorizationRequirement : IAuthorizationRequirement { }

//public class ResourceAuthorizationHandler : AuthorizationHandler<ResourceAuthorizationRequirement, string>
//{
//    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAuthorizationRequirement requirement, string resource)
//    {
//        // Check if the user is authenticated
//        if (!context.User.Identity.IsAuthenticated)
//        {
//            return Task.CompletedTask;
//        }

//        // Check if the user is in the "Admin" role
//        if (context.User.IsInRole("Admin"))
//        {
//            context.Succeed(requirement);
//            return Task.CompletedTask;
//        }

//        // Get the user's ID
//        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

//        // Get the resource owner's ID
//        //var resourceOwnerId = GetResourceOwnerId(resource);
//        using var dbContext = new IdealDiscussContext();
//        var resourceOwnerId = await GetResourceOwnerId<TResource>(dbContext, resource);

//        // Check if the user is the resource owner
//        if (userId == resourceOwnerId)
//        {
//            context.Succeed(requirement);
//        }

//        return Task.CompletedTask;
//    }

//    private async Task<string> GetResourceOwnerId<TResource>(IdealDiscussContext dbContext, string resourceId)
//    where TResource : BaseEntity, new()
//    {
//        var resource = await dbContext.Set<TResource>().FindAsync(resourceId);

//        if (resource == null)
//        {
//            throw new ArgumentException($"Resource with ID {resourceId} does not exist.");
//        }

//        var owner = await dbContext.Users.FindAsync(resource.Id);

//        if (owner == null)
//        {
//            throw new ArgumentException($"User with ID {resource.Id} does not exist.");
//        }

//        return owner.Id;
//    }

//}

