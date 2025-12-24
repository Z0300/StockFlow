using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Infrastructure.Messaging;

public sealed class IntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    public IntegrationEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    public Task PublishAsync(object integrationEvent, CancellationToken ct)
    {
        return _publishEndpoint.Publish(integrationEvent, ct);
    }
}
