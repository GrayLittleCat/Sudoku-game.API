using SharedKernel;

namespace Domain.Players;

public sealed record UserCreatedDomainEvent(int PlayerId) : IDomainEvent;
