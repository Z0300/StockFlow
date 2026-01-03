using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;

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

        builder.HasMany(t => t.TransferItem)
               .WithOne()
               .HasForeignKey(i => i.TransferId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(x => x.SourceWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Warehouse>()
           .WithMany()
           .HasForeignKey(x => x.DestinationWarehouseId)
           .OnDelete(DeleteBehavior.Restrict);

    }
}

