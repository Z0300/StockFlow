using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Orders.GetById;

public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<GetOrderByIdResponse>;
