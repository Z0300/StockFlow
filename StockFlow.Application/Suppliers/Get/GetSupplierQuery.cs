using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Application.Suppliers.Get;

public sealed record GetSupplierQuery : IQuery<List<SupplierResponse>>;
