using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Suppliers;

namespace StockFlow.Application.Suppliers.Update;

internal sealed class UpdateSupplierCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateSupplierCommand>
{
    public async Task<Result> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
    {
        Supplier? supplier = await context.Suppliers
            .SingleOrDefaultAsync(x => x.Id == command.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound(command.SupplierId));
        }

        supplier.Name = command.Name;
        supplier.ContactInfo = command.ContactInfo;
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
