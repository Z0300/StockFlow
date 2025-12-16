using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Application.Suppliers.GetById;

public sealed record GetSupplierByIdQuery(Guid SupplierId) : IQuery<SupplierResponse>;
