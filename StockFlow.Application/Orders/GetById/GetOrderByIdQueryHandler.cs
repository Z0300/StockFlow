using System.Data;
using Dapper;
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
                    a.created_at AS OrderDate,
                    a.received_at AS ReceivedDate,
                    a.order_status AS OrderStatus,

                    b.id AS WarehouseId,
                    b.name AS WarehouseName,
                    b.location AS WarehouseLocation,

                    c.id AS SupplierId,
                    c.name AS SupplierName,
                    c.contact_info AS SupplierContactInfo, 

                    p.id AS ProductId,
                    p.name AS ProductName,
                    d.quantity AS Quantity,
                    d.unit_price_amount AS UnitPrice,
                    d.unit_price_currency AS Currency

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
                        new { query.OrderId },
                        splitOn: "WarehouseId,SupplierId,ProductId");

        OrderResponse? order = orders.FirstOrDefault();

        if (order is null)
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound);
        }

        return order;
    }
}
