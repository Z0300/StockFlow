using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Categories.Get;

internal sealed class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, IReadOnlyList<CategoryResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public GetCategoryQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<IReadOnlyList<CategoryResponse>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    id AS CategoryId,
                    name AS CategoryName,
                    description AS CategoryDescription
                FROM categories
                """;

        IEnumerable<CategoryResponse> categories = await connection.QueryAsync<CategoryResponse>(sql);

        return categories.ToList();
    }
}
