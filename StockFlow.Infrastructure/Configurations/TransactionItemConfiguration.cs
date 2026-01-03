using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Shared;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.ToTable("transaction_items");

        builder.HasKey(it => it.Id);

        builder.Property(x => x.Id)
               .HasConversion(transactionItemId => transactionItemId.Value, value => new TransactionItemId(value));

        builder.Property(x => x.QuantityChange)
               .IsRequired();

        builder.OwnsOne(u => u.UnitCost, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
               .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<Product>()
          .WithMany()
          .HasForeignKey(it => it.ProductId);


        builder.HasIndex(x => new { x.ProductId });
    }
}
