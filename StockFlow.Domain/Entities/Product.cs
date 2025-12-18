using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class Product : Entity
{
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

}
