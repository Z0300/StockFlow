using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
            .HasConversion(orderId => orderId.Value, value => new OrderId(value));

        builder.HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(u => u.WarehouseId);

        builder.HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(u => u.SupplierId);

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
