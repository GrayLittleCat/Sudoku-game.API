using SharedKernel;

namespace Domain.Players;

public static class PlayerErrors
{
    public static Error NotFound(int userId) => new(
        "Users.NotFound", $"The user with the Id = '{userId}' was not found");

    public static Error NotFoundByEmail(string email) => new(
        "Users.NotFoundByEmail", $"The user with the Email = '{email}' was not found");
}
