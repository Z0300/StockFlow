using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using StockFlow.Infrastructure.Authentication;

namespace StockFlow.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionAuthorizationHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {

        if (!context.User.Identity!.IsAuthenticated)
        {
            return;
        }

        using IServiceScope scope = _serviceProvider.CreateScope();

        AuthorizationService authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

        Guid userId = context.User.GetUserId();

        HashSet<string> permissions = await authorizationService.GetPermissionsForUserAsync(userId);

        if (permissions.Contains(requirement.Permissions))
        {
            context.Succeed(requirement);
        }
    }
}
