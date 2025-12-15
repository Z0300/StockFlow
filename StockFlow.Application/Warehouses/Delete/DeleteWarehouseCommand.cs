using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;


namespace StockFlow.Application.Warehouses.Delete;

public sealed record DeleteWarehouseCommand(Guid WarehouseId) : ICommand;
