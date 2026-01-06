using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Users.ValueObjects;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override void Add(User user)
    {
        foreach (Role role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }

    public void AddRefresToken(RefreshToken refreshToken)
            => DbContext.Add(refreshToken);

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<User>()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<RefreshToken>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(user => user.Token == refreshToken, cancellationToken);
    }

    public async Task RevokeRefreshTokenAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await DbContext
           .Set<RefreshToken>()
           .Where(x => x.UserId == new UserId(userId))
           .ExecuteDeleteAsync(cancellationToken);
    }
}
