using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Categories.GetById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryResponse>;
