using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Suppliers;

namespace StockFlow.Application.Suppliers.Update;

internal sealed class UpdateSupplierCommandHandler
    : ICommandHandler<UpdateSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        Supplier? category = await _supplierRepository.GetByIdAsync(new SupplierId(request.SupplierId), cancellationToken);

        if (category is null)
        {
            return Result.Failure(SupplierErrors.NotFound);
        }

        category.ChangeName(request.Name);
        category.ChangeContactInfo(request.ContactInfo);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
