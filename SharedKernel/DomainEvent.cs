using MediatR;

namespace SharedKernel;

public record DomainEvent(int Id) : INotification;
