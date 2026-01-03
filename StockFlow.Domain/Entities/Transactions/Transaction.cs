using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Transactions;

public class Transaction : Entity<TransactionId>
{
    private Transaction(
        TransactionId id,
        WarehouseId warehouseId,
        TransactionType transactionType,
        List<TransactionItem> items,
        string? reason,
        OrderId? orderId,
        TransferId? transferId,
        DateTime createdAt) : base(id)
    {
        Id = id;
        WarehouseId = warehouseId;
        TransactionType = transactionType;
        TransactionItems = items;
        Reason = reason;
        OrderId = orderId;
        TransferId = transferId;
        CreatedAt = createdAt;
    }

    protected Transaction() { }

    public WarehouseId WarehouseId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public string? Reason { get; private set; }
    public OrderId? OrderId { get; private set; }
    public TransferId? TransferId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<TransactionItem> TransactionItems { get; private set; }

    public static Transaction Create(
            WarehouseId warehouseId,
            TransactionType transactionType,
            string? reason,
            OrderId? orderId,
            TransferId? transferId,
            DateTime createdAt,
            List<TransactionItem> items)
    {
        return new Transaction(
            TransactionId.New(),
            warehouseId,
            transactionType,
            items,
            reason,
            orderId,
            transferId,
            createdAt);
    }
}
