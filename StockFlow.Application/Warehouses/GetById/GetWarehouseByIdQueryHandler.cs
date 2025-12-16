using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Application.Warehouses.GetById;

internal sealed class GetWarehouseByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetWarehouseByIdQuery, WarehouseResponse>
{
    public async Task<Result<WarehouseResponse>> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
    {
        WarehouseResponse? warehouse = await context.Warehouses
            .AsNoTracking()
            .Where(warehouse => warehouse.Id == query.WarehouseId)
            .Select(w => new WarehouseResponse
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure<WarehouseResponse>(WarehouseErrors.NotFound(query.WarehouseId));
        }

        return warehouse;
    }
}
