using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Domain.Entities.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    void Add(User user);
}
