using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transfers;

namespace StockFlow.Domain.Entities.TransferItems;

public class TransferItem : Entity<TransferItemId>
{
    public TransferItem(ProductId productId, int requestedQuantity)
    {
        ProductId = productId;
        RequestedQuantity = requestedQuantity;
        ReceivedQuantity = 0;
    }
    protected TransferItem() { }

    public TransferId TransferId { get; set; }
    public ProductId ProductId { get; private set; }
    public int RequestedQuantity { get; private set; }
    public int? ReceivedQuantity { get; private set; }
   
}
