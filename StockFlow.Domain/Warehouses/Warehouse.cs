using SharedKernel;

namespace StockFlow.Domain.Warehouses;

public sealed class Warehouse : Entity
{
    public required string Name { get; set; }
    public required string Location { get; set; }
}