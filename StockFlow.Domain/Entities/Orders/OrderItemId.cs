using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities.Orders;

public record OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.NewGuid());
}
