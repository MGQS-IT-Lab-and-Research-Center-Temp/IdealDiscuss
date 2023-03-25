using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdealDiscuss.Middlewares;

public class RoleBasedAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public RoleBasedAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requiredRole = context.GetEndpoint()?.Metadata?.GetMetadata<RoleRequirement>()?.Role;

        if (requiredRole != null)
        {
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != requiredRole)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
                return;
            }
        }

        await _next(context);
    }
}

public class RoleRequirement : IAuthorizationRequirement
{
    public RoleRequirement(string role)
    {
        Role = role;
    }

    public string Role { get; }
}

