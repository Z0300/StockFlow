using StockFlow.Domain.Enums;
using StockFlow.Domain.Orders;
using StockFlow.Domain.Products;

namespace StockFlow.Domain.InventoryTransactions;

public sealed class InventoryTransaction
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int QuantityChange { get; set; }
    public TransactionType TransactionType { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public DateTime Timestamp { get; set; }
}