using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Application.Warehouses.GetById;

public sealed record GetWarehouseByIdQuery(Guid WarehouseId) : IQuery<WarehouseResponse>;
