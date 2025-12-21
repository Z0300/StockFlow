using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Enums;

public enum TransferStatus
{
    Draft = 1,
    InTransit = 2,
    Completed = 3,
    Cancelled = 4
}
