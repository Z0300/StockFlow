using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;

namespace StockFlow.Application.Categories.GetById;

internal sealed class GetCategoryByIdQueryHandler
    : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetCategoryByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    id AS CategoryId,
                    name AS CategoryName,
                    description AS CategoryDescription
                FROM categories
                WHERE id = @CategoryId
                """;

        CategoryResponse category = await connection.QueryFirstOrDefaultAsync<CategoryResponse>(sql,
            new
            {
                request.CategoryId
            });

        if (category == null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);
        }

        return category;
    }
}
