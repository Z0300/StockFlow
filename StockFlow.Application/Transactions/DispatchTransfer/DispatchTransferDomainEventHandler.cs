using MediatR;
using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Entities.TransferItems;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Transfers.Events;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;



namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferDomainEventHandler : INotificationHandler<TransferDispatchedEvent>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITransactionRepository _transactionRepository;

    public DispatchTransferDomainEventHandler(
    ITransferRepository transferRepository,
    IDateTimeProvider dateTimeProvider,
    ITransactionRepository transactionRepository)
    {
        _transferRepository = transferRepository;
        _dateTimeProvider = dateTimeProvider;
        _transactionRepository = transactionRepository;
    }
    public async Task Handle(TransferDispatchedEvent notification, CancellationToken cancellationToken)
    {
        Transfer? transfer = await _transferRepository.GetByIdAsync(notification.TransferId, cancellationToken);

        if (transfer is null)
        {
            return;
        }

        IReadOnlyCollection<Transaction> transctions = Transaction.CreateMany(
            transfer.SourceWarehouseId,
            TransactionType.TransferOut,
            null,
            null,
            transfer.Id,
            _dateTimeProvider.UtcNow,
            transfer.Items.Select<TransferItem, (ProductId, int, Money?)>(i =>
                (i.ProductId, i.ReceivedQuantity ?? 0, null)));

        await _transactionRepository.BulkInsertAsync(transctions, cancellationToken);
    }
}
