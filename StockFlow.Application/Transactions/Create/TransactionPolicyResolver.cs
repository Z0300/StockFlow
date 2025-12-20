using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Transactions.Create.PolicyResolver;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create;

internal sealed class TransactionPolicyResolver
{
    private readonly IReadOnlyDictionary<TransactionType, ITransactionPolicyResolver> _policies;

    public TransactionPolicyResolver(IReadOnlyDictionary<TransactionType, ITransactionPolicyResolver> policies)
    {
        _policies = policies;
    }

    public TransactionPolicyResolver(IEnumerable<ITransactionPolicyResolver> policies)
    {
        _policies = policies.ToDictionary(p => p.Type);
    }

    public ITransactionPolicyResolver Resolve(TransactionType type)
    {
        if (!_policies.TryGetValue(type, out ITransactionPolicyResolver? policy))
        {
            throw new DomainException($"No policy defined for {type}");
        }
        return policy;
    }
}
