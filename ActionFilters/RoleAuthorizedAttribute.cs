using Microsoft.AspNetCore.Authorization;

namespace IdealDiscuss.ActionFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleAuthorizeAttribute : AuthorizeAttribute
{
    public RoleAuthorizeAttribute(string role) : base()
    {
        Role = role;
    }

    public string Role
    {
        get => base.Policy;
        set => base.Policy = $"RequireRole:{value}";
    }
}


