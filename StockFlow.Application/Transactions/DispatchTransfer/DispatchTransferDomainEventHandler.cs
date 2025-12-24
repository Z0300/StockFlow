using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Domain.DomainEvents;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;



namespace StockFlow.Application.Transactions.DispatchTransfer;

internal sealed class DispatchTransferDomainEventHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider) : IDomainEventHandler<DispatchTransferDomainEvent>
{
    public async Task HandleAsync(DispatchTransferDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Transfer transfer = await context.Transfers
                    .Include(t => t.Items)
                    .SingleOrDefaultAsync(t => t.Id == domainEvent.TransferId, cancellationToken) ??
               throw new DomainException($"Transfer with Id {domainEvent.TransferId} not found.");

        var transactionId = Guid.NewGuid();

        List<Transaction> transactions = [.. transfer.Items.Select(item =>
            new Transaction
            {
                OperationId = transactionId,
                ProductId = item.ProductId,
                WarehouseId = transfer.SourceWarehouseId,
                QuantityChange = -item.RequestedQuantity,
                TransactionType = TransactionType.TransferOut,
                CreatedAt = dateTimeProvider.UtcNow,
                TransferId = transfer.Id
            })];

        await context.Transactions.BulkInsertOptimizedAsync(transactions,
            options => options.IncludeGraph = true);
    }
}
