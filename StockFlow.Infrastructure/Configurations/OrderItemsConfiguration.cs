using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Shared;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
           .HasConversion(orderItemId => orderItemId.Value, value => new OrderItemId(value));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(u => u.Quantity).IsRequired();

        builder.OwnsOne(u => u.UnitPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
               .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

    }
}
