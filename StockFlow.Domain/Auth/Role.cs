using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;
using StockFlow.Domain.Users;

namespace StockFlow.Domain.Auth;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role InventoryManager = new(2, "InventoryManager");
    public static readonly Role WarehouseStaff = new(3, "WarehouseStaff");
    public static readonly Role Auditor = new(4, "Auditor");
    public static readonly Role Procurement = new(5, "Procurement");
    public static readonly Role SystemIntegration = new(6, "SystemIntegration ");

    private Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }

    public ICollection<User> Users { get; set; }

}
