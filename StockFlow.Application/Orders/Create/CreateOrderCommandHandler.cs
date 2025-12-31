using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Orders.Enums;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Domain.Shared;

namespace StockFlow.Application.Orders.Create;

internal sealed class CreateOrderCommandHandler
    : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(
                new WarehouseId(command.WarehouseId),
                new SupplierId(command.SupplierId),
                OrderStatus.Pending,
                _dateTimeProvider.UtcNow,
                [.. command.Items
                   .Select(item => OrderItem.Create(
                        new ProductId(item.ProductId),
                        item.Quantity,
                        new Money(item.UnitPrice, Currency.Php)))]);

        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id.Value;
    }
}
