using System.Data;
using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;

namespace StockFlow.Application.Orders.GetById;

internal sealed class GetOrderByIdQueryHandler
    : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetOrderByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _connectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    a.id AS OrderId,
                    a.order_date AS OrderDate,
                    a.total_amount AS OrderTotalAmount,
                    a.status AS OrderStatus,

                    b.id AS WarehouseId,
                    b.name AS WarehouseName,
                    b.location AS WarehouseLocation,

                    c.id AS SupplierId,
                    c.name AS SupplierName,
                    c.contact_info AS SupplierContactInfo,

                    p.id AS ProductId,
                    p.name AS ProductName,
                    d.quantity AS Quantity,
                    d.unit_price AS UnitPrice

                FROM orders a
                INNER JOIN warehouses b ON a.warehouse_id = b.id
                INNER JOIN suppliers c ON a.supplier_id = c.id
                LEFT JOIN order_items d ON a.id = d.order_id
                LEFT JOIN products p ON d.product_id = p.id
                WHERE a.id = @OrderId;
                """;

        IEnumerable<OrderResponse> orders = await connection
            .QueryAsync<OrderResponse, 
                        WarehouseResponse, 
                        SupplierResponse, 
                        OrderItemResponse, 
                        OrderResponse>(sql,
                        (order, warehouse, supplier, orderItem) =>
                        {
                            order.Warehouse = warehouse;
                            order.Supplier = supplier;
                            order.OrderItems ??= [];
                            order.OrderItems.Add(orderItem);
                            return order;
                        },
                        splitOn: "WarehouseId,SupplierId,ProductId");

        OrderResponse? order = orders.FirstOrDefault();

        if (order is null)
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound);
        }

        return order;
    }
}
