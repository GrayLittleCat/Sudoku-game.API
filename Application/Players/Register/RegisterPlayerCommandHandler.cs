using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Domain.Players;

namespace Application.Players.Register;

internal sealed class RegisterPlayerCommandHandler
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
        var identityId = await _authenticationService.RegisterAsync(
            request.Email,
            request.Password);

        var customer = new Player(
            request.Email,
            request.Name,
            identityId);

        _playerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
