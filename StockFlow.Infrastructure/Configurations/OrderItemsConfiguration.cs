using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.OrderItems;
using StockFlow.Domain.Products;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasOne(oi => oi.Product).WithMany().HasForeignKey(oi => oi.ProductId);

        builder.Property(u => u.Quantity).IsRequired();
        
        builder.Property(u => u.UnitPrice).HasPrecision(18, 2).IsRequired();
    }
}