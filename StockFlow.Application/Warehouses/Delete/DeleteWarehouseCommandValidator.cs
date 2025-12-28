using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Application.Warehouses.Delete;

internal sealed class DeleteWarehouseCommandValidator
    : ICommandHandler<DeleteWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteWarehouseCommandValidator(
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await _warehouseRepository.GetByIdAsync(new WarehouseId(request.WarehouseId), cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure(WarehouseErrors.NotFound);
        }

        _warehouseRepository.Remove(warehouse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
