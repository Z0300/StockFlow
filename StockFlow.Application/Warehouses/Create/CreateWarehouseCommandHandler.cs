using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Warehouses.Create;

internal sealed class CreateWarehouseCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateWarehouseCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        if (await context.Warehouses.AnyAsync(w => w.Name == command.Name, cancellationToken))
        {
            return Result.Failure<Guid>(WarehouseErrors.NameNotUnique);
        }

        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Location = command.Location
        };

        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync(cancellationToken);

        return warehouse.Id;
    }
}
