using StockFlow.Domain.Entities.Transfers;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class TransferRepository : Repository<Transfer, TransferId>, ITransferRepository
{
    public TransferRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


}
