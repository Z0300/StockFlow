using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Application.Warehouses.Get;

internal sealed class GetWarehouseQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetWarehouseQuery, List<WarehouseResponse>>
{
    public async Task<Result<List<WarehouseResponse>>> Handle(GetWarehouseQuery query, CancellationToken cancellationToken)
    {
        List<WarehouseResponse> warehouses = await context.Warehouses
            .AsNoTracking()
            .Select(w => new WarehouseResponse
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location
            })
            .ToListAsync(cancellationToken);

        return warehouses;
    }
}
