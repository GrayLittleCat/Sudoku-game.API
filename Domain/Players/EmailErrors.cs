using SharedKernel;

namespace Domain.Players;

public static class EmailErrors
{
    public static readonly Error Empty = new("Email.Empty", "Email is empty");

    public static readonly Error InvalidFormat = new(
        "Email.InvalidFormat", "Email format is invalid");
}
