using SharedKernel;

namespace StockFlow.Domain.DomainErrors;

public static class WarehouseErrors
{
    public static readonly Error NameNotUnique = Error.Conflict(
       "Warehouses.NameNotUnique",
       "The provided name is not unique");

    public static Error NotFound(Guid warehouseId) => Error.NotFound(
       "Warehouses.NotFound",
       $"The warehouse with the Id = '{warehouseId}' was not found");
}
