using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Products.Get;

internal sealed class GetProductQueryHandler
    : IQueryHandler<GetProductQuery, IReadOnlyList<ProductsResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public GetProductQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<IReadOnlyList<ProductsResponse>>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    id AS ProductId,
                    name AS ProductName,
                    sku AS ProductSku,
                    price AS ProductPrice,
                FROM products
                """;

        IEnumerable<ProductsResponse> products = await connection.QueryAsync<ProductsResponse>(sql);

        return products.ToList();
    }
}
