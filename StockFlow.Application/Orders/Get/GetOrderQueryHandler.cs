using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Orders.Get;

internal sealed class GetOrderQueryHandler
    : IQueryHandler<GetOrderQuery, IReadOnlyCollection<OrdersResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetOrderQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<IReadOnlyCollection<OrdersResponse>>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                  id AS OrderId,
                  order_date AS OrderDate,
                  total_amount AS OrderTotalAmount,
                  status AS OrderStatus
                FROM orders  
                """;

        IEnumerable<OrdersResponse> orders = await connection.QueryAsync<OrdersResponse>(sql);
        return orders.ToList();
    }
}
