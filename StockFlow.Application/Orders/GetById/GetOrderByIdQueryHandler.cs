using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;

namespace StockFlow.Application.Orders.GetById;

internal sealed class GetOrderByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResponse>
{
    public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        GetOrderByIdResponse? order = await context.Orders
             .AsNoTracking()
             .Where(o => o.Id == query.OrderId)
             .Select(o => new GetOrderByIdResponse
             {
                 Id = o.Id,
                 OrderDate = o.CreatedAt,
                 SupplierName = o.Supplier != null ? o.Supplier.Name : string.Empty,
                 TotalAmount = o.OrderItems.Sum(item => item.UnitPrice * item.Quantity),
                 Status = o.OrderStatus.ToString(),
                 OrderItems = o.OrderItems.Select(oi => new OrderItemResponse
                 {
                     ProductId = oi.ProductId,
                     ProductName = oi.Product!.Name,
                     Quantity = oi.Quantity,
                     UnitPrice = oi.UnitPrice
                 }).ToList()
             })
             .SingleOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure<GetOrderByIdResponse>(OrderErrors.NotFound(query.OrderId));
        }

        return order;
    }
}
