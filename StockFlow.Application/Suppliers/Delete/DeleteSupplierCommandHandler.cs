using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Suppliers;

namespace StockFlow.Application.Suppliers.Delete;

internal sealed class DeleteSupplierCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteSupplierCommand>
{
    public async Task<Result> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
    {
        Supplier? supplier = await context.Suppliers
            .SingleOrDefaultAsync(x => x.Id == command.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound(command.SupplierId));
        }

        context.Suppliers.Remove(supplier);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
