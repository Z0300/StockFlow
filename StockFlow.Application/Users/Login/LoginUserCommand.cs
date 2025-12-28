using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
