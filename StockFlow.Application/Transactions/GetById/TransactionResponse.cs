using StockFlow.Domain.Entities.Transactions.Enums;

namespace StockFlow.Application.Transactions.GetById;

public sealed class TransactionResponse
{
    public Guid TransactionId { get; init; }
    public TransactionWarehouseResponse Warehouse { get; set; }
    public TransactionItemsResponse Items { get; set; }
    public TransactionType TransactionType { get; init; }
    public string? Reason { get; init; }
    public Guid? OrderId { get; init; }
    public Guid? TransferId { get; init; }
}
