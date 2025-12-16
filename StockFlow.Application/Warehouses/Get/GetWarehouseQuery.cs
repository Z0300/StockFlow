using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Application.Warehouses.Get;

public sealed class GetWarehouseQuery : IQuery<List<WarehouseResponse>>;

