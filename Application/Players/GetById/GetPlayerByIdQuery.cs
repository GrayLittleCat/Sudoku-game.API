using Application.Abstractions.Messaging;

namespace Application.Players.GetById;

public sealed record GetPlayerByIdQuery(int PlayerId) : IQuery<PlayerResponse>;

public sealed record PlayerResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string IdentityId { get; init; }
}
