using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Shared;

namespace StockFlow.Domain.Entities.TransactionItems;

public class TransactionItem : Entity<TransactionItemId>
{
    private TransactionItem(
        TransactionItemId id,
        ProductId productId,
        int quantityChange,
        Money? unitCost) : base(id)
    {
        ProductId = productId;
        QuantityChange = quantityChange;
        UnitCost = unitCost;
    }

    protected TransactionItem() { }

    public TransactionId TransactionId { get; set; }
    public ProductId ProductId { get; private set; }
    public int QuantityChange { get; private set; }
    public Money? UnitCost { get; private set; }
    public Transaction Transaction { get; set; }


    public static TransactionItem Create(
            ProductId productId,
            int quantityChange,
            Money? unitCost)
    {
        return new TransactionItem(
            TransactionItemId.New(),
            productId,
            quantityChange,
            unitCost);
    }
}
