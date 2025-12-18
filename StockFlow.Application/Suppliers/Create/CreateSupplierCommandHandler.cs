using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.DomainErrors;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Suppliers.Create;

internal sealed class CreateSupplierCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateSupplierCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        if (await context.Suppliers.AnyAsync(s => s.Name == command.Name, cancellationToken))
        {
            return Result.Failure<Guid>(SupplierErrors.NameNotUnique);
        }

        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            ContactInfo = command.ContactInfo
        };

        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync(cancellationToken);

        return supplier.Id;
    }
}

