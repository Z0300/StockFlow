using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.Delete;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand;
