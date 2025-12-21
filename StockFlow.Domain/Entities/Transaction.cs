using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

public sealed class Transaction : Entity
{
    public Guid OperationId { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }


    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }


    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }


    public int QuantityChange { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal? UnitCost { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid? TransferId { get; set; }
    public Transfer? Transfer { get; set; }
}
