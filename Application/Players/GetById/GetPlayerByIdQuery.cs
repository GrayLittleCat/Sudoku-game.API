using Application.Abstractions.Messaging;

namespace Application.Players.GetById;

public sealed record GetPlayerByIdQuery(int PlayerId) : IQuery<PlayerResponse>;
