using Application.Abstractions.Messaging;

namespace Application.Players.Login;

public record LoginCommand(string Email, string Password) :ICommand<string>;
