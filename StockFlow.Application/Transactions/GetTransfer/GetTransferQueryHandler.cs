using System.Data;
using Dapper;
using Dapper.SimpleSqlBuilder;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Transactions.GetTransfer;

internal sealed class GetTransferQueryHandler
    : IQueryHandler<GetTransferQuery, IReadOnlyCollection<TransfersResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTransferQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<IReadOnlyCollection<TransfersResponse>>> Handle(GetTransferQuery request, CancellationToken cancellationToken)
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
                        t.received_at AS ReceivedAt
                    FROM transfers t 
                    INNER JOIN warehouses sw ON sw.id = t.source_warehouse_id  
                    INNER JOIN warehouses dw ON dw.id = t.destination_warehouse_id");

        IEnumerable<TransfersResponse> transfers = await connection.QueryAsync<TransfersResponse>(builder.Sql, builder.Parameters);

        return transfers.ToList();
    }
}
