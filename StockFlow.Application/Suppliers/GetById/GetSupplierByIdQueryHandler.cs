using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Suppliers.GetById;

internal sealed class GetSupplierByIdQueryHandler
    : IQueryHandler<GetSupplierByIdQuery, SupplierResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetSupplierByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<SupplierResponse>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
             SELECT 
                id AS SupplierId,
                name AS SupplierName,
                contact_info AS SupplierContactInfo
            FROM suppliers
            WHERE id = @SupplierId;
            """;

        SupplierResponse? supplier = await connection.QueryFirstOrDefaultAsync<SupplierResponse>(sql, new
        {
            request.SupplierId
        });

        return supplier;
    }


}

