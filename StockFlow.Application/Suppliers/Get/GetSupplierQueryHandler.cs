using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Suppliers.Get;

internal sealed class GetSupplierQueryHandler
    : IQueryHandler<GetSupplierQuery, IReadOnlyList<SupplierResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public GetSupplierQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result<IReadOnlyList<SupplierResponse>>> Handle(GetSupplierQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
             SELECT 
                id AS SupplierId,
                name AS SupplierName,
                contact_info AS SupplierContactInfo
            FROM suppliers
            """;

        IEnumerable<SupplierResponse> suppliers = await connection.QueryAsync<SupplierResponse>(sql);

        return suppliers.ToList();
    }
}
