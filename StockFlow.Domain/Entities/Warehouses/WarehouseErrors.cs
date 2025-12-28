using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Warehouses;

public static class WarehouseErrors
{
    public static readonly Error NameNotUnique = new(
       "Warehouses.NameNotUnique",
       "The name provided is not unique.");

    public static readonly Error NotFound = new(
       "Warehouses.NotFound",
       $"The warehouse item with the specified identifier was not found");
}
