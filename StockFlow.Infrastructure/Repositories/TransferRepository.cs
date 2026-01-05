using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class TransferRepository : Repository<Transfer, TransferId>, ITransferRepository
{
    public TransferRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Transfer> GetByIdWithItemsAsync(TransferId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Transfer>()
            .Include(x => x.TransferItem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
