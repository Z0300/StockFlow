using Microsoft.AspNetCore.Authorization;

namespace StockFlow.Infrastructure.Authorization;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permissions)
    {
        Permissions = permissions;
    }

    public string Permissions => field;
}
