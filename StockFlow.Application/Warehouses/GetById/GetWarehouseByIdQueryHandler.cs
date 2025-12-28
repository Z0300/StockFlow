using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Warehouses.GetById;

internal sealed class GetWarehouseByIdQueryHandler
    : IQueryHandler<GetWarehouseByIdQuery, WarehouseResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetWarehouseByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<WarehouseResponse>> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    id AS WarehouseId,
                    name AS WarehouseName,
                    location AS WarehouseLocation
                FROM warehouses
                WHERE id = @CategoryId
                """;

        WarehouseResponse warehouse = await connection.QueryFirstOrDefaultAsync<WarehouseResponse>(sql);

        return warehouse;
    }
}
