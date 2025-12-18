using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class Inventory : Entity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public int Quantity { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = [];
}
