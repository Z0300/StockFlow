using System.Data;
using System.Net.NetworkInformation;
using Dapper;
using Dapper.SimpleSqlBuilder;
using Dapper.SimpleSqlBuilder.Extensions;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Shared;

namespace StockFlow.Application.Transactions.Get;

internal sealed class GetTransactionQueryHandler
    : IQueryHandler<GetTransactionQuery, PagedList<TransactionReponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTransactionQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<PagedList<TransactionReponse>>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        int offset = (request.Page - 1) * request.PageSize;

        Builder builder = SimpleBuilder.Create($@"
                            SELECT 
                              t.id AS TransactionId,
                              p.name AS Product,
                              w.name AS Warehouse,
                              ti.quantity_change AS QuantityChange,
                              t.transaction_type AS TransactionType
                            FROM transactions t
                            INNER JOIN transaction_items ti ON t.id = ti.transaction_id 
                            RIGHT JOIN products p ON ti.product_id = p.id
                            INNER JOIN warehouses w ON t.warehouse_id = w.id
                            WHERE 
                                t.created_at >= {request.From} AND t.created_at < {request.To.AddDays(1)}
                                AND ({request.Type} IS NULL OR t.transaction_type = {request.Type})
                                AND ({request.ProductName} IS NULL OR p.name LIKE '%' || {request.ProductName} || '%')
                            ORDER BY t.created_at DESC
                            OFFSET {offset} LIMIT {request.PageSize}");

        IEnumerable<TransactionReponse> rows = await connection.QueryAsync<TransactionReponse>(builder.Sql, builder.Parameters);

        var transactions = PagedList<TransactionReponse>.CreateFromEnumerable(
                rows,
                request.Page,
                request.PageSize);

        return transactions;
    }
}
