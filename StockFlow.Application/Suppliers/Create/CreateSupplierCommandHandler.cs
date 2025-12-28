using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Exceptions;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Suppliers;

namespace StockFlow.Application.Suppliers.Create;

internal sealed class CreateSupplierCommandHandler
    : ICommandHandler<CreateSupplierCommand, Guid>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        if (await _supplierRepository.IsNameUnique(command.Name, cancellationToken))
        {
            return Result.Failure<Guid>(SupplierErrors.NameNotUnique);
        }

        try
        {
            var supplier = Supplier.Create(
                command.Name,
                command.ContactInfo
            );

            _supplierRepository.Add(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return supplier.Id.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(SupplierErrors.NameNotUnique);
        }
    }
}

