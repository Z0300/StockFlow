using SharedKernel;

namespace StockFlow.Domain.DomainErrors;

public static class CategoryErrors
{
    public static Error NameNotUnique => Error.Conflict(
        "Category.NameNotUnique",
        $"The name provided is not unique.");

    public static Error NotFound(Guid categoryId) => Error.NotFound(
        "Category.NotFound",
        $"The category item with the Id = '{categoryId}' was not found");
}
