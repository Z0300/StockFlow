using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.TransferItems;
using StockFlow.Domain.Entities.Transfers.Enums;
using StockFlow.Domain.Entities.Transfers.Events;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Transfers;

public class Transfer : Entity<TransferId>
{
    private Transfer(
        TransferId id,
        WarehouseId sourceWarehouseId,
        WarehouseId destinationWarehouseId,
        TransferStatus transferStatus,
        DateTime createdAt,
        DateTime? dispatchAt,
        DateTime? receivedAt,
        List<TransferItem> items,
        List<Transaction>? transactions) : base(id)
    {
        Id = id;
        SourceWarehouseId = sourceWarehouseId;
        DestinationWarehouseId = destinationWarehouseId;
        Status = transferStatus;
        CreatedAt = createdAt;
        DispatchAt = dispatchAt;
        ReceivedAt = receivedAt;
        TransferItem = items;
        Transactions = transactions;
    }

    protected Transfer() { }

    public WarehouseId SourceWarehouseId { get; private set; }
    public WarehouseId DestinationWarehouseId { get; private set; }
    public TransferStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DispatchAt { get; private set; }
    public DateTime? ReceivedAt { get; private set; }
    public List<TransferItem> TransferItem { get; private set; }
    public List<Transaction>? Transactions { get; private set; }

    public static Transfer CreateTransfer(
        WarehouseId sourceWarehouseId,
        WarehouseId destinationWarehouseId,
        DateTime createdAt,
        IEnumerable<TransferItem> items)
    {
        var transfer = new Transfer(
            TransferId.New(),
            sourceWarehouseId,
            destinationWarehouseId,
            TransferStatus.Draft,
            createdAt,
            null,
            null,
            [.. items],
            null);

        return transfer;
    }



    public Result Dispatch(DateTime dispatchAt)
    {
        if (Status != TransferStatus.Draft)
        {
            return Result.Failure(TransferErrors.AlreadyDispatch);
        }

        Status = TransferStatus.InTransit;
        DispatchAt = dispatchAt;

        RaiseDomainEvent(new TransferDispatchedEvent(Id));

        return Result.Success();
    }


}
