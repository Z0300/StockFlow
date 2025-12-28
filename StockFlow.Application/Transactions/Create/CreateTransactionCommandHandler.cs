using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;


namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandHandler
    : ICommandHandler<CreateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _transactionRepository = transactionRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {


        IEnumerable<(ProductId ProductId, int Quantity, Money? UnitCost)> items = request.Items.Select(i =>
            (
                ProductId: new ProductId(i.ProductId),
                Quantity: i.QuantityChange,
                UnitCost: (Money?)new Money(i.UnitCost, Currency.Php)
            ));


        IReadOnlyCollection<Transaction> transctions = Transaction.CreateMany(
            new WarehouseId(request.WarehouseId),
            request.TransactionType,
            request.Reason,
            request.OrderId is not null ? new OrderId(request.OrderId.Value) : null,
            null,
            _dateTimeProvider.UtcNow,
            items);

        await _transactionRepository.BulkInsertAsync(transctions, cancellationToken);

        return transctions.FirstOrDefault()!.Id.Value;
    }
}
