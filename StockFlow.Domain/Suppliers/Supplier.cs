using SharedKernel;

namespace StockFlow.Domain.Suppliers;

public sealed class Supplier : Entity
{
    public required string Name { get; set; }
    public required string ContactInfo { get; set; }
}