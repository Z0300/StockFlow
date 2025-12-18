using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.Update;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Sku,
    decimal Price,
    Guid? CategoryId) : ICommand;
