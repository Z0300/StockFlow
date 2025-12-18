using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.InventoryTransactions.ReceiveOrder;

internal sealed class ReceiveOrderCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ReceiveOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReceiveOrderCommand command, CancellationToken cancellationToken)
    {
        var transactionId = Guid.NewGuid();

        List<Transaction> transactions = [.. command.Items.Select(item =>
            new Transaction
            {
                TransactionGroupId = transactionId,
                QuantityChange = item.QuantityChange,
                TransactionType = TransactionType.PurchaseReceipt,
                OrderId = item.OrderId,
                CreatedAt = DateTime.UtcNow
            })];


        await context.Transactions.BulkInsertOptimizedAsync(transactions,
              options => options.IncludeGraph = true);

        return transactionId;
    }
}
