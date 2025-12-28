using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "Users.Found",
        $"The user with the specified identifier was not found");

    public static readonly Error EmailNotUnique = new(
        "Users.EmailNotUnique",
        "The provided email is not unique");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}
