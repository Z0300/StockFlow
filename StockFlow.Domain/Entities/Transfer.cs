using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

public sealed class Transfer : Entity
{
    public Guid SourceWarehouseId { get; set; }
    public Guid DestinationWarehouseId { get; set; }


    public TransferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<TransferItem> Items { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
