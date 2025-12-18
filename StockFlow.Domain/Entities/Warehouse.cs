using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class Warehouse : Entity
{
    public required string Name { get; set; }
    public required string Location { get; set; }
}