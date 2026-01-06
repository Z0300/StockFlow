namespace StockFlow.Domain.Entities.Users;

public record RefreshTokenId(Guid Value)
{
    public static RefreshTokenId New() => new(Guid.NewGuid());
}
