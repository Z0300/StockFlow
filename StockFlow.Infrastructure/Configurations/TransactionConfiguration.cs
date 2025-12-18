using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(it => it.Id);

        builder.HasIndex(it => it.TransactionGroupId);

        builder.HasOne(it => it.Order)
            .WithMany()
            .HasForeignKey(it => it.OrderId);

        builder.Property(it => it.UnitCost)
            .HasPrecision(18, 2);

        builder.Property(it => it.Reason)
            .HasMaxLength(512);

    }
}
