using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Transfers;

public interface ITransferRepository
{
    Task<Transfer> GetByIdAsync(TransferId id, CancellationToken cancellationToken = default);
    void Add(Transfer transfer);
}
