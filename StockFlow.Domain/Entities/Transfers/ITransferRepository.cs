namespace StockFlow.Domain.Entities.Transfers;

public interface ITransferRepository
{
    Task<Transfer> GetByIdAsync(TransferId id, CancellationToken cancellationToken = default);
    void Add(Transfer transfer);
}
