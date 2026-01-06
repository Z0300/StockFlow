using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Users.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommand(string RefreshToken) : ICommand<TokenResponse>;
