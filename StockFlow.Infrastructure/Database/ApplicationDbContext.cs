using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Domain.Categories;
using StockFlow.Domain.InventoryTransactions;
using StockFlow.Domain.OrderItems;
using StockFlow.Domain.Orders;
using StockFlow.Domain.Products;
using StockFlow.Domain.Suppliers;
using StockFlow.Domain.Users;
using StockFlow.Domain.Warehouses;
using StockFlow.Infrastructure.DomainEvents;

namespace StockFlow.Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set;}
    public DbSet<InventoryTransaction> InventoryTransactions { get; set;}
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set;}
    public DbSet<Product> Products { get; set;}
    public DbSet<Supplier> Suppliers { get;set; }
    public DbSet<Warehouse> Warehouses { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        var result = await base.SaveChangesAsync(cancellationToken);
 
        await PublishDomainEventsAsync();

        return result;
    }
    
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        
        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}