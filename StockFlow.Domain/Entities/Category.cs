using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class Category : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}