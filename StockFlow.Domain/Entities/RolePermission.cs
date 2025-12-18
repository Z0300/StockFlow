namespace StockFlow.Domain.Entities;

public sealed class RolePermission
{
    public required int RoleId { get; set; }
    public required int PermissionId { get; set; }
}
