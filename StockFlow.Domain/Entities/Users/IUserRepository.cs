using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Domain.Entities.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<RefreshToken> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeRefreshTokenAsync(Guid userId, CancellationToken cancellationToken = default);
    void Add(User user);
    void AddRefresToken(RefreshToken refreshToken);
}
