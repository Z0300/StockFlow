using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;
using StockFlow.Domain.InventoryTransactions;

namespace StockFlow.Application.InventoryTransactions.ReceiveOrder;

internal sealed class ReceiveOrderCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ReceiveOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReceiveOrderCommand command, CancellationToken cancellationToken)
    {
        var transactionId = Guid.NewGuid();

        List<InventoryTransaction> transactions = [.. command.Items.Select(item =>
            new InventoryTransaction
            {
                TransactionId = transactionId,
                ProductId = item.ProductId,
                WarehouseId = item.WarehouseId,
                QuantityChange = item.QuantityChange,
                TransactionType = TransactionType.PurchaseReceipt,
                OrderId = item.OrderId,
                CreatedAt = DateTime.UtcNow
            })];


        await context.InventoryTransactions.BulkInsertOptimizedAsync(transactions,
              options => options.IncludeGraph = true);

        return transactionId;
    }
}
