using MediatR;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
