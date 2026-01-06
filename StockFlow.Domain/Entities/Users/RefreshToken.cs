using System.ComponentModel.DataAnnotations;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Users;

public sealed class RefreshToken
{
    private RefreshToken(
        RefreshTokenId id,
        string token,
        UserId userId,
        DateTime expiresOnUtc)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiresOnUtc = expiresOnUtc;
    }

    private RefreshToken() { }
    public RefreshTokenId Id { get; private set; }
    public string Token { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }
    public User User { get; set; }
    public static RefreshToken Create(string token, UserId userId, DateTime expiresOnUtc)
            => new(RefreshTokenId.New(), token, userId, expiresOnUtc);

    public Result NewRefreshToken(string token, DateTime expiresOnUtc)
    {
        Token = token;
        ExpiresOnUtc = expiresOnUtc;
        return Result.Success();
    }

}
