using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;


namespace StockFlow.Application.Transactions.Create;

internal sealed class CreateTransactionCommandHandler
    : ICommandHandler<CreateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {

        var transaction = Transaction.Create(
            new WarehouseId(request.WarehouseId),
            request.TransactionType,
            request.Reason,
            new OrderId(request.OrderId!.Value),
            null,
            _dateTimeProvider.UtcNow,
            [.. request.Items
                .Select(item => TransactionItem.Create(
                    new ProductId(item.ProductId),
                    item.QuantityChange,
                    item.UnitCost.HasValue
                        ? new Money(item.UnitCost.Value, Currency.Php)
                        : null))]);

        _transactionRepository.Add(transaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transaction.Id.Value;
    }
}
