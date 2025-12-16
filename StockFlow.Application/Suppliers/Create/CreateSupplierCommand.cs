using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Suppliers.Create;

public sealed record CreateSupplierCommand(string Name, string ContactInfo) : ICommand<Guid>;
