using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace StockFlow.Domain.Entities;

public sealed class TransferItem : Entity
{
    public Guid TransferId { get; set; }
    public Transfer? Transfer { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int RequestedQuantity { get; set; }
    public int ReceivedQuantity { get; set; }
}
