using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Transfers;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("transfers");

        builder.HasKey(t => t.Id);

        builder.Property(x => x.Id)
           .HasConversion(transferId => transferId.Value, value => new TransferId(value));

        builder.Property(t => t.CreatedAt)
               .IsRequired();

        builder.HasMany(t => t.Transactions)
               .WithOne()
               .HasForeignKey(tx => tx.TransferId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Items)
               .WithOne()
               .HasForeignKey(i => i.TransferId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

