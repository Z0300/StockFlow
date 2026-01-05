namespace StockFlow.Infrastructure.Outbox;

public class OutboxMessage
{
    public OutboxMessage(Guid id, DateTime ocurredOnUtc, string type, string content)
    {
        Id = id;
        OccurredOnUtc = ocurredOnUtc;
        Type = type;
        Content = content;
    }

    protected OutboxMessage()
    {
    }

    public Guid Id { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }

    /// <summary>
    /// Name of the Domain Event
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// JSON string containing the serialized domain event
    /// </summary>
    public string Content { get; private set; }
    public DateTime? ProcessedOnUtc { get; set; }
    public string? Error { get; set; }
}
