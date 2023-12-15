namespace Application.Players;

public sealed record PlayerResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string IdentityId { get; init; }
}
