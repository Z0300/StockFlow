using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class TransferRepository : Repository<Transfer, TransferId>, ITransferRepository
{
    public TransferRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

  
}
