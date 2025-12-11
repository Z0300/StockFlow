using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Products;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name).HasMaxLength(255).IsRequired();
        
        builder.Property(u => u.Sku).HasMaxLength(15).IsRequired();
        
        builder.Property(u => u.Price).HasPrecision(18, 2).IsRequired();
        
        builder.HasOne(u => u.Category)
            .WithMany()
            .HasForeignKey(u => u.CategoryId);
        
        builder.HasOne(u => u.Warehouse)
            .WithMany()
            .HasForeignKey(u => u.WarehouseId);
        
        
    }
}
