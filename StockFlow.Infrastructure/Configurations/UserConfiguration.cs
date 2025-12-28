using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
          .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(u => u.FirstName)
            .HasMaxLength(200);

        builder.Property(u => u.LastName)
            .HasMaxLength(200);

        builder.Property(x => x.Email)
          .HasMaxLength(400)
          .HasConversion(email => email.Value, value => new Email(value))
          .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(512)
            .IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();

    }
}
