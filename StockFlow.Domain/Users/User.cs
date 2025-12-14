using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Users;

public sealed class User : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}
