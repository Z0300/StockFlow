using StockFlow.Domain.Entities;

namespace StockFlow.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}