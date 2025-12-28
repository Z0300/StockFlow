using StockFlow.Application.Abstractions.Clock;

namespace StockFlow.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
