using System.Data;
using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;

namespace StockFlow.Application.Products.GetById;

internal sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public GetProductByIdQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    p.id AS ProductIdId,
                    p.name AS ProductName,
                    p.sku AS ProductSku,
                    p.price_amount AS ProductPrice,
                    p.price_currency AS ProductCurrency,

                    c.id AS CategoryId,
                    c.name AS CategoryName,
                    c.description AS CategoryDescription

                FROM products p
                INNER JOIN categories c ON p.category_id = c.id
                WHERE p.id = @ProductId;
                """;

        IEnumerable<ProductResponse>? product = await connection.QueryAsync<ProductResponse, CategoryResponse, ProductResponse>(
             sql,
             (product, category) =>
             {
                 product.Category = category;
                 return product;

             }, new
             {
                 query.ProductId

             },
             splitOn: "CategoryId");

        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);
        }

        return product.FirstOrDefault();
    }
}
