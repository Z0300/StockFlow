using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities.Products;

public record ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
}
