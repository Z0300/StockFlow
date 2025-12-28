using StockFlow.Domain.Entities.Users;

namespace StockFlow.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}