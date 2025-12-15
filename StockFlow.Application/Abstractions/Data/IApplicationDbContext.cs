using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Auth;
using StockFlow.Domain.Categories;
using StockFlow.Domain.InventoryTransactions;
using StockFlow.Domain.OrderItems;
using StockFlow.Domain.Orders;
using StockFlow.Domain.Products;
using StockFlow.Domain.Suppliers;
using StockFlow.Domain.Users;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Category> Categories { get; }
    DbSet<InventoryTransaction> InventoryTransactions { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Order> Orders { get; }
    DbSet<Product> Products { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<Role> Roles { get; }
    DbSet<RolePermission> RolePermissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
