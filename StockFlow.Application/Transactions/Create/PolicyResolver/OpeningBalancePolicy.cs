using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class OpeningBalancePolicy(IApplicationDbContext context) : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.OpeningBalance;

    public async Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        foreach (TransactionItems item in command.Items)
        {
            bool exists = await context.Transactions
                .AnyAsync(t =>
                    t.TransactionType == TransactionType.OpeningBalance &&
                    t.ProductId == item.ProductId &&
                    t.WarehouseId == command.WarehouseId,
                    ct);

            if (exists)
            {
                throw new DomainException(
                    "Opening balance already exists");
            }
        }
    }
}
