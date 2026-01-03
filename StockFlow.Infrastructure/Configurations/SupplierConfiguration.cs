using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Suppliers;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
          .HasConversion(supplierId => supplierId.Value, value => new SupplierId(value));

        builder.Property(u => u.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.ContactInfo)
            .HasMaxLength(255);

    }
}
