using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.InventoryTransactions;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.HasKey(it => it.Id);

        builder.Property(it => it.TransactionId)
            .IsRequired();

        builder.HasOne(it => it.Warehouse)
            .WithMany()
            .HasForeignKey(it => it.WarehouseId);

        builder.HasOne(it => it.Product)
            .WithMany()
            .HasForeignKey(it => it.ProductId);

        builder.HasOne(it => it.Order)
            .WithMany()
            .HasForeignKey(it => it.OrderId);

        builder.Property(it => it.UnitCost)
            .HasPrecision(18, 2);

        builder.Property(it => it.Reason)
            .HasMaxLength(512);

    }
}
