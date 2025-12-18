using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Warehouses.Delete;

internal sealed class DeleteWarehouseCommandValidator(IApplicationDbContext context)
    : ICommandHandler<DeleteWarehouseCommand>
{
    public async Task<Result> Handle(DeleteWarehouseCommand command, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await context.Warehouses
            .SingleOrDefaultAsync(c => c.Id == command.WarehouseId, cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure(WarehouseErrors.NotFound(command.WarehouseId));
        }

        context.Warehouses.Remove(warehouse);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
