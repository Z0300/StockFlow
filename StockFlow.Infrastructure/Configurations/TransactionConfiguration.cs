using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(it => it.Id);

        builder.Property(x => x.TransactionType)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.QuantityChange)
               .IsRequired();

        builder.Property(x => x.OperationId)
               .IsRequired();

        builder.Property(it => it.UnitCost)
               .HasPrecision(18, 4);

        builder.Property(it => it.Reason)
               .HasMaxLength(512);

        builder.HasOne(it => it.Order)
               .WithMany()
               .HasForeignKey(it => it.OrderId);

        builder.HasOne(it => it.Product)
               .WithMany()
               .HasForeignKey(it => it.ProductId)
               .IsRequired();

        builder.HasOne(it => it.Warehouse)
               .WithMany()
               .HasForeignKey(it => it.WarehouseId)
               .IsRequired();

        builder.HasOne(t => t.Transfer)
               .WithMany(tr => tr.Transactions)
               .HasForeignKey(t => t.TransferId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ProductId, x.WarehouseId });
        builder.HasIndex(it => it.OperationId);
        builder.HasIndex(x => x.OrderId);
    }
}

