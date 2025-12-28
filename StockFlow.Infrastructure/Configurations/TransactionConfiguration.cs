using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(it => it.Id);

        builder.Property(x => x.Id)
           .HasConversion(transactionId => transactionId.Value, value => new TransactionId(value));

        builder.Property(x => x.QuantityChange)
               .IsRequired();

        builder.OwnsOne(u => u.UnitCost, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
               .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.Property(it => it.Reason)
               .HasMaxLength(512);

        builder.HasOne<Order>()
               .WithMany()
               .HasForeignKey(it => it.OrderId);

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(it => it.ProductId);

        builder.HasOne<Warehouse>()
               .WithMany()
               .HasForeignKey(it => it.WarehouseId)
               .IsRequired();

        builder.HasOne<Transfer>()
               .WithMany(tr => tr.Transactions)
               .HasForeignKey(t => t.TransferId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ProductId, x.WarehouseId });
        builder.HasIndex(x => x.OrderId);
    }
}

