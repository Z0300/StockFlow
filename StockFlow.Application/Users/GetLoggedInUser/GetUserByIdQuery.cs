using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Users.GetLoggedInUser;

public sealed record GetUserByIdQuery : IQuery<UserResponse>;
