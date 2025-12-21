using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class TransferItemConfiguration : IEntityTypeConfiguration<TransferItem>
{
    public void Configure(EntityTypeBuilder<TransferItem> builder)
    {
        builder.HasKey(it => it.Id);

        builder.Property(i => i.RequestedQuantity)
               .IsRequired();

        builder.Property(i => i.ReceivedQuantity)
               .IsRequired();

        builder.HasIndex(i => new { i.TransferId, i.ProductId })
               .IsUnique();

        builder.HasOne(it => it.Product)
               .WithMany()
               .HasForeignKey(it => it.ProductId)
               .IsRequired();

        builder.HasOne(it => it.Transfer)
               .WithMany(t => t.Items) 
               .HasForeignKey(it => it.TransferId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

