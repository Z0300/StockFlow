using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Warehouses.Get;

internal sealed class GetWarehouseQueryHandler
    : IQueryHandler<GetWarehouseQuery, IReadOnlyList<WarehouseResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public GetWarehouseQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<IReadOnlyList<WarehouseResponse>>> Handle(GetWarehouseQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    id AS WarehouseId,
                    name AS WarehouseName,
                    location AS WarehouseLocation
                FROM warehouses
                """;

        IEnumerable<WarehouseResponse> warehouses = await connection.QueryAsync<WarehouseResponse>(sql);

        return warehouses.ToList();
    }
}
