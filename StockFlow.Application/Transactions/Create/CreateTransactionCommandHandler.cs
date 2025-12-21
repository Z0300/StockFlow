using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.Create.PolicyResolver;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandHandler(
    IApplicationDbContext context,
    TransactionPolicyResolver resolver)
    : ICommandHandler<CreateTransactionCommand, Guid>
{

    public async Task<Result<Guid>> Handle(
        CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        ITransactionPolicyResolver policy = resolver.Resolve(command.TransactionType);

        await policy.ValidateAsync(command, cancellationToken);

        var transactionId = Guid.NewGuid();

        List<Transaction> transactions = [.. command.Items.Select(item =>
            new Transaction
            {
                OperationId = transactionId,
                ProductId = item.ProductId,
                WarehouseId = command.WarehouseId,
                QuantityChange = item.QuantityChange,
                UnitCost = item.UnitCost,
                TransactionType = command.TransactionType,
                Reason = command.Reason,
                OrderId = command.OrderId,
                CreatedAt = DateTime.UtcNow
            })];


        await context.Transactions.BulkInsertOptimizedAsync(transactions,
              options => options.IncludeGraph = true);

        return transactionId;
    }
}
