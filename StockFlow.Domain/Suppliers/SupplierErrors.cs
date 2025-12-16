using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace StockFlow.Domain.Suppliers;

public static class SupplierErrors
{
    public static Error NameNotUnique => Error.Conflict(
       "Suppliers.NameNotUnique",
       $"The name provided is not unique.");

    public static Error NotFound(Guid supplierId) => Error.NotFound(
      "Suppliers.NotFound",
      $"The supplier item with the Id = '{supplierId}' was not found");
}
