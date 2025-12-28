using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Shared;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
         .HasConversion(productId => productId.Value, value => new ProductId(value));

        builder.Property(u => u.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Sku)
            .HasMaxLength(15)
            .IsRequired();

        builder.OwnsOne(u => u.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
               .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(u => u.CategoryId);

        builder.HasIndex(u => u.Name)
            .IsUnique();

    }
}
