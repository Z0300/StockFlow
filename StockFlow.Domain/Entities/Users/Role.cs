namespace StockFlow.Domain.Entities.Users;

public sealed class Role
{
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role InventoryManager = new(2, "InventoryManager");
    public static readonly Role WarehouseStaff = new(3, "WarehouseStaff");
    public static readonly Role Auditor = new(4, "Auditor");
    public static readonly Role Procurement = new(5, "Procurement");
    public static readonly Role SystemIntegration = new(6, "SystemIntegration ");

    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; init; }
    public string Name { get; init; }
    public ICollection<User> Users { get; set; }
    public ICollection<Permission> Permissions { get; set; }

}
