using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;

namespace StockFlow.Domain.Entities.Transactions;

public class Transaction : Entity<TransactionId>
{
    private Transaction(
        TransactionId id,
        ProductId productId,
        WarehouseId warehouseId,
        int quantityChange,
        TransactionType transactionType,
        Money? unitCost,
        string? reason,
        OrderId? orderId,
        TransferId? transferId,
        DateTime createdAt) : base(id)
    {
        Id = id;
        ProductId = productId;
        WarehouseId = warehouseId;
        QuantityChange = quantityChange;
        TransactionType = transactionType;
        UnitCost = unitCost;
        Reason = reason;
        OrderId = orderId;
        TransferId = transferId;
        CreatedAt = createdAt;
    }

    protected Transaction() { }

    public ProductId ProductId { get; private set; }
    public WarehouseId WarehouseId { get; private set; }
    public int QuantityChange { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public Money? UnitCost { get; private set; }
    public string? Reason { get; private set; }
    public OrderId? OrderId { get; private set; }
    public TransferId? TransferId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static IReadOnlyCollection<Transaction> CreateMany(
            WarehouseId warehouseId,
            TransactionType transactionType,
            string? reason,
            OrderId? orderId,
            TransferId? transferId,
            DateTime createdAt,
            IEnumerable<(ProductId ProductId, int Quantity, Money? UnitCost)> items)
    {
        var transactions = new List<Transaction>();

        foreach ((ProductId ProductId, int Quantity, Money? UnitCost) item in items)
        {
            transactions.Add(new Transaction(
            TransactionId.New(),
            item.ProductId,
            warehouseId,
            item.Quantity,
            transactionType,
            item.UnitCost,
            reason,
            orderId,
            transferId,
            createdAt));
        }

        return transactions;
    }
}
