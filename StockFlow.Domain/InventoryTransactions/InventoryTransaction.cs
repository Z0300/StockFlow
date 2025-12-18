using SharedKernel;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Orders;
using StockFlow.Domain.Products;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Domain.InventoryTransactions;

public sealed class InventoryTransaction : Entity
{
    public Guid TransactionId { get; set; }

    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int QuantityChange { get; set; }
    public TransactionType TransactionType { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal? UnitCost { get; set; }
    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }
}
