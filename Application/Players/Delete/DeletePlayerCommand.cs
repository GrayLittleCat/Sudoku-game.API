using Application.Abstractions.Messaging;

namespace Application.Players.Delete;

public record DeletePlayerCommand(int PlayerId) : ICommand;
