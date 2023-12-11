using Application.Abstractions.Messaging;

namespace Application.Players.Delete;

public record DeletePlayerCommand(int PlayerId) : ICommand;

public record DeletePlayerRequest(int PlayerId);
