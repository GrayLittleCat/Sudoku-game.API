using MediatR;

namespace Application.Players.Register;


public record RegisterPlayerCommand(string Email, string Password, string Name) : IRequest;

public record RegisterCustomerRequest(string Email, string Password, string Name);

