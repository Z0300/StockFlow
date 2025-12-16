using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Suppliers.Update;

public sealed record UpdateSupplierCommand(Guid SupplierId,
    string Name,
    string ContactInfo
) : ICommand;

