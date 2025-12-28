using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.TransferItems;
using StockFlow.Domain.Entities.Transfers;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransferItemConfiguration : IEntityTypeConfiguration<TransferItem>
{
    public void Configure(EntityTypeBuilder<TransferItem> builder)
    {
        builder.ToTable("transfer_items");

        builder.HasKey(it => it.Id);

        builder.Property(x => x.Id)
           .HasConversion(transferItemId => transferItemId.Value, value => new TransferItemId(value));

        builder.Property(i => i.RequestedQuantity)
               .IsRequired();

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(it => it.ProductId);

        builder.HasOne<Transfer>()
               .WithMany(t => t.Items)
               .HasForeignKey(it => it.TransferId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => new { i.TransferId, i.ProductId })
            .IsUnique();
    }
}

