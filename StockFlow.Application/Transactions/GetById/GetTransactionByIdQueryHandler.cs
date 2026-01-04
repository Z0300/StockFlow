using System.Data;
using Dapper;
using Dapper.SimpleSqlBuilder;
using Microsoft.AspNetCore.Connections;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Transactions;

namespace StockFlow.Application.Transactions.GetById;

internal sealed class GetTransactionByIdQueryHandler
    : IQueryHandler<GetTransactionByIdQuery, TransactionResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTransactionByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<TransactionResponse>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        Builder builder = SimpleBuilder.Create($@"
                            SELECT 
                                t.id AS TransactionId,
                                t.transaction_type AS TransactionType,

                                w.id AS WarehouseId,
                                w.name AS Warehouse,

                                ti.id AS TransactionItemId,
                                ti.product_id AS ProductId,
                                p.name AS ProductName,
                                ti.quantity_change AS QuantityChange,
                                ti.unit_cost_amount AS UnitCost,                           
                                ti.unit_cost_currency AS Currency
                            FROM 
                                transactions t
                            INNER JOIN 
                                transaction_items ti ON t.id = ti.transaction_id 
                            RIGHT JOIN 
                                products p ON ti.product_id = p.id
                            INNER JOIN 
                                warehouses w ON t.warehouse_id = w.id
                            WHERE
                                t.id = {request.TransactionId}");


        IEnumerable<TransactionResponse> transactions = await connection
            .QueryAsync<TransactionResponse,
                        TransactionWarehouseResponse,
                        TransactionItemsResponse,
                        TransactionResponse>(builder.Sql, (transaction, warehouse, items) =>
                        {
                            transaction.Warehouse = warehouse;
                            transaction.Items = items;
                            return transaction;

                        }, builder.Parameters,
                        splitOn: "WarehouseId,TransactionItemId");

        TransactionResponse transaction = transactions.FirstOrDefault();

        if (transaction is null)
        {
            return Result.Failure<TransactionResponse>(TransactionErrors.NotFound);
        }

        return transaction;
    }
}
