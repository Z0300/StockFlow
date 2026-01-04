namespace StockFlow.Application.Transactions.GetById;

public sealed class TransactionWarehouseResponse
{
    public Guid WarehouseId { get; init; }
    public string Warehouse { get; init; }
}
