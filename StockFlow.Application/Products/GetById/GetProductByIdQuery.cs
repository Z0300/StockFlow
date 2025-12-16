using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResponse>;
