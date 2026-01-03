using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.TransferItems;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(it => it.Id);

        builder.Property(x => x.Id)
           .HasConversion(transactionId => transactionId.Value, value => new TransactionId(value));

        builder.Property(it => it.Reason)
               .HasMaxLength(512);

        builder.HasOne<Order>()
               .WithMany()
               .HasForeignKey(it => it.OrderId);

        builder.HasOne<Warehouse>()
               .WithMany()
               .HasForeignKey(it => it.WarehouseId)
               .IsRequired();

        builder.HasOne<Transfer>()
               .WithMany(tr => tr.Transactions)
               .HasForeignKey(t => t.TransferId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.TransactionItems)
               .WithOne(x => x.Transaction)
               .HasForeignKey(i => i.TransactionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.WarehouseId);
        builder.HasIndex(x => x.OrderId);
    }
}

