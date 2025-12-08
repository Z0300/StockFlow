using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;