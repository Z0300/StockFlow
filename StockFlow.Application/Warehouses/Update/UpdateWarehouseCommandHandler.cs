using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Application.Warehouses.Update;

internal sealed class UpdateWarehouseCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateWarehouseCommand>
{
    public async Task<Result> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await context.Warehouses
            .SingleOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure(WarehouseErrors.NotFound(command.Id));
        }

        warehouse.Name = command.Name;
        warehouse.Location = command.Location;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
