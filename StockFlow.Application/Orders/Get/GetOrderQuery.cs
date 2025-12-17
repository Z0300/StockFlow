using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Orders.Get;

public sealed class GetOrderQuery : IQuery<List<GetOrderResponse>>;
