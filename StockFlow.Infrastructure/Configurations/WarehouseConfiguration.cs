using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name).HasMaxLength(255).IsRequired();
        
        builder.Property(u => u.Location).HasMaxLength(255);
    }
}