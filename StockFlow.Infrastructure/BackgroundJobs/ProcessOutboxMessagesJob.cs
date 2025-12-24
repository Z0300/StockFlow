using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using SharedKernel;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Infrastructure;
using StockFlow.Infrastructure.Database;
using StockFlow.Infrastructure.Outbox;

namespace StockFlow.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IIntegrationEventPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IIntegrationEventPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }
    public async Task Execute(IJobExecutionContext context)
    {

        List<OutboxMessage> messages = await _dbContext.Set<OutboxMessage>()
             .Where(m => m.ProcessedOnUtc == null)
             .OrderBy(m => m.OccurredOnUtc)
             .Take(20)
             .ToListAsync(context.CancellationToken);


        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            await _publisher.PublishAsync(domainEvent, context.CancellationToken);

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
