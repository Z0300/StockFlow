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

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<User>()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}
