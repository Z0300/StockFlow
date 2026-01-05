using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Caching;
using StockFlow.Domain.Entities.Users;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _context;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(Guid userId)
    {
        string cacheKey = $"auth:roles-{userId}";

        UserRolesResponse? cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        UserRolesResponse roles = await _context.Set<User>()
            .Where(user => user.Id == new UserId(userId))
            .Select(user => new UserRolesResponse
            {
                Id = user.Id.Value,
                Roles = user.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(Guid userId)
    {
        string cacheKey = $"auth:permissions-{userId}";

        HashSet<string>? cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachedPermissions is not null)
        {
            return cachedPermissions;
        }

        ICollection<Permission> permissions = await _context.Set<User>()
            .Where(user => user.Id == new UserId(userId))
            .SelectMany(user => user.Roles.Select(role => role.Permissions))
            .FirstAsync();

        var permissionsSet = permissions.Select(p => p.Name).ToHashSet();

        await _cacheService.SetAsync(cacheKey, permissionsSet);

        return permissionsSet;
    }
}
