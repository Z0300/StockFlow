namespace StockFlow.Domain.Entities.Categories;

public record CategoryId(Guid Value)
{
    public static CategoryId New() => new(Guid.NewGuid());
}
