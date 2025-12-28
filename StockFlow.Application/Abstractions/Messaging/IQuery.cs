using MediatR;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
