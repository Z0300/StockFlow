using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Orders.Get;

internal sealed class GetOrderQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrderQuery, List<GetOrderResponse>>
{
    public async Task<Result<List<GetOrderResponse>>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        List<GetOrderResponse> orders = await context.Orders
            .AsNoTracking()
            .Select(order => new GetOrderResponse
            {
                Id = order.Id,
                OrderDate = order.CreatedAt,
                SupplierName = order.Supplier != null ? order.Supplier.Name : string.Empty,
                TotalAmount = order.OrderItems.Sum(item => item.UnitPrice * item.Quantity),
                Status = order.OrderStatus.ToString()
            })
            .ToListAsync(cancellationToken);


        return orders;
    }
}
