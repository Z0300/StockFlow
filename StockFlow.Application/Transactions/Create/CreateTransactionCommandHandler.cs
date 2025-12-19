using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateTransactionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var transactionId =  Guid.NewGuid();

        List<Transaction> transactions = [.. command.Items.Select(item =>
            new Transaction
            {
                TransactionGroupId = transactionId,
                ProductId = item.ProductId,
                WarehouseId = item.WarehouseId,
                QuantityChange = item.QuantityChange,
                UnitCost = item.UnitCost,
                TransactionType = item.TransactionType,
                Reason = item.Reason,
                OrderId = item.OrderId,
                CreatedAt = DateTime.UtcNow
            })];


        await context.Transactions.BulkInsertOptimizedAsync(transactions,
              options => options.IncludeGraph = true);

        return transactionId;
    }
}
