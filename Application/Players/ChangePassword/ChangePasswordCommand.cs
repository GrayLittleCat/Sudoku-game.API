using Application.Abstractions.Messaging;

namespace Application.Players.ChangePassword;

public record ChangePasswordCommand(int PlayerId, string NewPassword) : ICommand;

public record ChangePasswordRequest(string NewPassword);
