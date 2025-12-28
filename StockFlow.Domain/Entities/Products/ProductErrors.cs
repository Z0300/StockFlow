using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Products;

public static class ProductErrors
{
    public static readonly Error NameNotUnique = new(
        "Products.NameNotUnique",
        "The name provided is not unique.");

    public static readonly Error NotFound = new(
      "Products.NotFound",
      "The product item with the specified identifier was not found");
}
