using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.TransferItems;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Application.Transactions.TransferOut;

internal sealed class CreateTransferOutCommandHandler
    : ICommandHandler<CreateTransferOutCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransferRepository _transferRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateTransferOutCommandHandler(
        IUnitOfWork unitOfWork,
        ITransferRepository transferRepository,
        ITransactionRepository transactionRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _transferRepository = transferRepository;
        _transactionRepository = transactionRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(CreateTransferOutCommand request, CancellationToken cancellationToken)
    {

        foreach (TransferOutItems item in request.Items)
        {
            int availableQuantity = await _transactionRepository.GetAvailableQuantity(
                new WarehouseId(request.SourceWarehouseId),
                new ProductId(item.ProductId),
                cancellationToken);

            if (availableQuantity < item.RequestedQuantity)
            {
                return Result.Failure<Guid>(TransferErrors.InsufficientStock);
            }
        }


        var transfer = Transfer.CreateTransfer(
             new WarehouseId(request.SourceWarehouseId),
             new WarehouseId(request.DestinationWarehouseId),
             _dateTimeProvider.UtcNow,
             request.Items.Select(i =>
                  TransferItem.Create(
                     new ProductId(i.ProductId),
                     i.RequestedQuantity)));


        _transferRepository.Add(transfer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transfer.Id.Value;
    }
}
