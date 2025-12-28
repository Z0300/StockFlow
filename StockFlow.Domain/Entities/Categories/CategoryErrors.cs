using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Categories;

public static class CategoryErrors
{
    public static readonly Error NameNotUnique = new(
        "Category.NameNotUnique",
        "The name provided is not unique.");

    public static readonly Error NotFound = new(
        "Category.NotFound",
        "The category item with the specified identifier was not found");
}
