using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Application.Warehouses.Update;

internal sealed class UpdateWarehouseCommandHandler
    : ICommandHandler<UpdateWarehouseCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWarehouseCommandHandler(
       IWarehouseRepository warehouseRepository,
       IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        Warehouse? warehouse = await _warehouseRepository.GetByIdAsync(new WarehouseId(command.Id), cancellationToken);

        if (warehouse is null)
        {
            return Result.Failure(WarehouseErrors.NotFound);
        }

        warehouse.ChangeName(command.Name);
        warehouse.ChangeLocation(command.Location);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
