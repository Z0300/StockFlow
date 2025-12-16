using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.InventoryTransactions;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Product)
            .WithMany()
            .HasForeignKey(u => u.ProductId);

        builder.HasOne(u => u.Order)
            .WithMany()
            .HasForeignKey(u => u.OrderId);

    }
}