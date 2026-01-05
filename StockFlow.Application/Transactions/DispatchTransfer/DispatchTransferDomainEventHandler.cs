using MediatR;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Transfers.Events;



namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferDomainEventHandler : INotificationHandler<TransferDispatchedEvent>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DispatchTransferDomainEventHandler(
    ITransferRepository transferRepository,
    IDateTimeProvider dateTimeProvider,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork)
    {
        _transferRepository = transferRepository;
        _dateTimeProvider = dateTimeProvider;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(TransferDispatchedEvent notification, CancellationToken cancellationToken)
    {
        Transfer? transfer = await _transferRepository
            .GetByIdWithItemsAsync(notification.TransferId, cancellationToken);

        if (transfer is null)
        {
            return;
        }

        var transctions = Transaction.Create(
            transfer.SourceWarehouseId,
            TransactionType.TransferOut,
            null,
            null,
            transfer.Id,
            _dateTimeProvider.UtcNow,
            [.. transfer.TransferItem.Select(item =>
                TransactionItem.Create(
                    item.ProductId,
                    item.ReceivedQuantity ?? 0,
                    null))]);

        _transactionRepository.Add(transctions);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
