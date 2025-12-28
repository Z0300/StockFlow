using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.Get;

public sealed record GetProductQuery : IQuery<IReadOnlyList<ProductsResponse>>;

