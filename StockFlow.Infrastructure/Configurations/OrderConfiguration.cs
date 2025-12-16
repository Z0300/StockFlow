using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Orders;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Warehouse)
            .WithMany()
            .HasForeignKey(u => u.WarehouseId);

        builder.HasOne(u => u.Supplier)
            .WithMany()
            .HasForeignKey(u => u.SupplierId);
    }
}