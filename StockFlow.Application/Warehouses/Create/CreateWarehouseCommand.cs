using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Warehouses.Create;

public sealed record CreateWarehouseCommand(string Name, string Location) : ICommand<Guid>;

