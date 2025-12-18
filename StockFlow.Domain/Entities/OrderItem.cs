using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class OrderItem : Entity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}