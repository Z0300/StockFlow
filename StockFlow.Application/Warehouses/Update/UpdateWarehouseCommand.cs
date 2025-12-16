using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Warehouses.Update;

public sealed record UpdateWarehouseCommand(
    Guid Id,
    string Name,
    string Location) : ICommand;
