using StockFlow.Domain.Entities.Users;

namespace StockFlow.Infrastructure.Authorization;

public sealed class UserRolesResponse
{
    public Guid Id { get; init; }
    public List<Role> Roles { get; init; } = [];
}
