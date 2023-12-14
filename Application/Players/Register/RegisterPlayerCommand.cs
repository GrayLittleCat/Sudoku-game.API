using Application.Abstractions.Messaging;

namespace Application.Players.Register;

public record RegisterPlayerCommand(string Email, string Password, string Name) : ICommand<int>;

public record RegisterPlayerRequest(string Email, string Password, string Name);
