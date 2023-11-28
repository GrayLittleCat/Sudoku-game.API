using System.Windows.Input;
using Domain.Players;
using MediatR;

namespace Application.Players.Register;


public record RegisterPlayerCommand(string Email, string Password, string Name) : IRequest;

public record RegisterPlayerRequest(string Email, string Password, string Name);

