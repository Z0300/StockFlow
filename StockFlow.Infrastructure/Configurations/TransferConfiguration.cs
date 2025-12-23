using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(t => t.CreatedAt)
               .IsRequired();

        builder.HasMany(t => t.Transactions)
               .WithOne(tx => tx.Transfer)
               .HasForeignKey(tx => tx.TransferId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Items)
               .WithOne(i => i.Transfer)
               .HasForeignKey(i => i.TransferId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

