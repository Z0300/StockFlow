using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users.Events;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Domain.Entities.Users;


public sealed class User : Entity<UserId>
{
    private readonly HashSet<Role> _roles = [];
    private User(
        UserId id,
        string firstName,
        string lastName,
        Email email,
        string passwordHash) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    private User() { }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles;

    public static User Create(string firstName, string lastName, Email email, string passwordHash)
    {
        var user = new User(UserId.New(), firstName, lastName, email, passwordHash);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

}
