using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Sku, 
    decimal Price,
    Guid CategoryId,
    Guid WarehouseId) : ICommand<Guid>;
