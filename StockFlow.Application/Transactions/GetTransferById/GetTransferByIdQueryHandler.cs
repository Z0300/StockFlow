using System.Data;
using Dapper;
using Dapper.SimpleSqlBuilder;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Transactions;

namespace StockFlow.Application.Transactions.GetTransferById;

internal sealed class GetTransferByIdQueryHandler
    : IQueryHandler<GetTransferByIdQuery, TransferResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTransferByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<TransferResponse>> Handle(GetTransferByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        Builder builder = SimpleBuilder.Create($@"
                   SELECT 
                        t.id AS TransferId,
                        sw.name AS SourceWarehouse,
                        dw.name AS DestinationWarehouse,
                        t.status AS Status,
                        t.created_at AS CreatedAt,
                        t.dispatch_at AS DispatchAt,
                        t.received_at AS ReceivedAt,
                        ti.id as TransferItemId,
                        p.name as Product,
                        ti.requested_quantity as RequestedQuantity,
                        ti.received_quantity as ReceivedQuantity
                    FROM transfers t 
                    INNER JOIN warehouses sw ON sw.id = t.source_warehouse_id  
                    INNER JOIN warehouses dw ON dw.id = t.destination_warehouse_id 
                    inner JOIN transfer_items ti ON t.id = ti.transfer_id 
                    LEFT JOIN products p ON p.id = ti.product_id 
                    WHERE t.id = {request.TransferId}");

        IEnumerable<TransferResponse> transfers = await connection
            .QueryAsync<TransferResponse,
                        TransferItemsResponse,
                        TransferResponse>(builder.Sql, (transfer, items) =>
                        {
                            transfer.Items ??= [];
                            transfer.Items.AddRange(items);
                            return transfer;

                        }, builder.Parameters,
                        splitOn: "TransferItemId");

        TransferResponse transfer = transfers.FirstOrDefault();

        if (transfer is null)
        {
            return Result.Failure<TransferResponse>(TransactionErrors.TransferNotFound);
        }

        return transfer;
    }
}
