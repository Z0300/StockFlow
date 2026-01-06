using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Users.RevokeRefreshToken;

public sealed record RevokeRefreshTokenCommand(Guid UserId) : ICommand<bool>;
