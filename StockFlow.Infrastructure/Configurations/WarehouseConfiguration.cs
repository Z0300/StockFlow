using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("warehouses");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
          .HasConversion(warehouseId => warehouseId.Value, value => new WarehouseId(value));

        builder.Property(u => u.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.Location)
            .HasMaxLength(200);
    }
}
