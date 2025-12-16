using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Suppliers.Delete;

public sealed record DeleteSupplierCommand(Guid SupplierId) : ICommand;

