using MediatR;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}
public interface IBaseCommand
{
}
