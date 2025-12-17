using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Orders.Cancel;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand;

