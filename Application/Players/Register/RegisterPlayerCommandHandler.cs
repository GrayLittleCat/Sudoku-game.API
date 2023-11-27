using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Players;
using MediatR;

namespace Application.Players.Register;

internal sealed class RegisterPlayerCommandHandler : IRequestHandler<RegisterPlayerCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPlayerCommandHandler(
        IAuthenticationService authenticationService,
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        

        var playerEmail = Email.Create(request.Email);
        if (!playerEmail.IsFailure)
        {
            return;
        }
        
        var identityId = await _authenticationService.RegisterAsync(
            request.Email,
            request.Password);
        
        var player = new Player(
            playerEmail.Value,
            new Name(request.Name),
            identityId);

        _playerRepository.Add(player);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
