using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Users;

namespace StockFlow.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();
        
        builder.Property(u => u.FirstName).HasMaxLength(255).IsRequired();
        
        builder.Property(u => u.LastName).HasMaxLength(255).IsRequired();
        
        builder.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();
        
        builder.Property(u => u.Role).IsRequired();
    }
}
