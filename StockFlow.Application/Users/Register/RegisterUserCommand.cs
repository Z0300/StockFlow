using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Users.Register;

public sealed record RegisterUserCommand(string FirstName, string LastName, string Email,  string Password)
    : ICommand<Guid>;
