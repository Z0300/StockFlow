using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Users;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
           .HasConversion(orderId => orderId.Value, value => new RefreshTokenId(value));

        builder.Property(r => r.Token).HasMaxLength(200);

        builder.HasIndex(r => r.Token).IsUnique();

        builder.HasOne(x => x.User).WithMany().HasForeignKey(r => r.UserId);

    }
}
