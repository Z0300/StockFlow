using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

public sealed class Transfer : Entity
{
    public Guid SourceWarehouseId { get; set; }
    public Guid DestinationWarehouseId { get; set; }


    public TransferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DispatchAt { get; set; }
    public DateTime? ReceivedAt { get; set; }

    public ICollection<TransferItem> Items { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
