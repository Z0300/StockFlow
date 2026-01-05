namespace StockFlow.Api.Endpoints.Transfers;

public sealed class CreateTransferRequest
{
    public required Guid SourceWarehouseId { get; init; }
    public required Guid DestinationWarehouseId { get; init; }
    public List<CreateTransferItemsRequest> Items { get; init; }
}

public sealed class CreateTransferItemsRequest
{
    public required Guid ProductId { get; init; }
    public required int RequestedQuantity { get; init; }
}




