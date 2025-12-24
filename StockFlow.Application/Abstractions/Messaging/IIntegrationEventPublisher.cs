namespace StockFlow.Application.Abstractions.Messaging;

public interface IIntegrationEventPublisher
{
    Task PublishAsync(object integrationEvent, CancellationToken ct);
}
