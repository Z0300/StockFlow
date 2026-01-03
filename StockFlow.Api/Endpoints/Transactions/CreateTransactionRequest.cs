using StockFlow.Domain.Entities.Transactions.Enums;

namespace StockFlow.Api.Endpoints.Transactions;

public sealed record CreateTransactionRequest
{
    public required Guid WarehouseId { get; init; }
    public Guid? OrderId { get; init; }
    public required TransactionType TransactionType { get; set; }
    public string? Reason { get; init; }
    public required List<CreateTransactionItemRequest> Items { get; init; }
};

public sealed record CreateTransactionItemRequest
{
    public required Guid ProductId { get; init; }
    public required int QuantityChange { get; init; }
    public decimal? UnitCost { get; init; }
}
