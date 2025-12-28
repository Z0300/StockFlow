using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Exceptions;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Application.Warehouses.Create;

internal sealed class CreateWarehouseCommandHandler
    : ICommandHandler<CreateWarehouseCommand, Guid>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateWarehouseCommandHandler(
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        if (await _warehouseRepository.IsNameUnique(request.Name, cancellationToken))
        {
            return Result.Failure<Guid>(WarehouseErrors.NameNotUnique);
        }

        try
        {
            var category = Warehouse.Create(
                request.Name,
                request.Location
            );

            _warehouseRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(WarehouseErrors.NameNotUnique);
        }
    }
}
