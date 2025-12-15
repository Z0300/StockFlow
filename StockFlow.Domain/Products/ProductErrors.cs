using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace StockFlow.Domain.Products;

public static class ProductErrors
{
    public static Error NameNotUnique => Error.Conflict(
        "Products.NameNotUnique",
        $"The name provided is not unique.");

    public static Error NotFound(Guid productId) => Error.NotFound(
      "Products.NotFound",
      $"The product item with the Id = '{productId}' was not found");
}
