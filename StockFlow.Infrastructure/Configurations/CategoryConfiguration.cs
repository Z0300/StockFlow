using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Categories;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasIndex(u => u.Name).IsUnique();
        
        builder.Property(u => u.Name).HasMaxLength(255).IsRequired();

        builder.Property(u => u.Description).HasMaxLength(512);
    }
}