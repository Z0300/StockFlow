using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Suppliers;

public static class SupplierErrors
{
    public static readonly Error NameNotUnique = new(
       "Suppliers.NameNotUnique",
       "The name provided is not unique.");

    public static readonly Error NotFound = new(
      "Suppliers.NotFound",
      $"The supplier item with the specified identifier was not found");
}
