using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Users;

public sealed class User : Entity
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Role Role { get; set; }  
}