using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transfers;

namespace StockFlow.Domain.Entities.TransferItems;

public class TransferItem : Entity<TransferItemId>
{
    private TransferItem(
        TransferItemId id,
        ProductId productId,
        int requestedQuantity,
        int receivedQuantity) : base(id)
    {
        ProductId = productId;
        RequestedQuantity = requestedQuantity;
        ReceivedQuantity = receivedQuantity;
    }
    protected TransferItem() { }

    public TransferId TransferId { get; protected set; }
    public ProductId ProductId { get; private set; }
    public int RequestedQuantity { get; private set; }
    public int? ReceivedQuantity { get; private set; }

    public static TransferItem Create(
        ProductId productId,
        int requestedQuantity)
    {
        return new TransferItem(
            TransferItemId.New(),
            productId,
            requestedQuantity,
            0);
    }

}
