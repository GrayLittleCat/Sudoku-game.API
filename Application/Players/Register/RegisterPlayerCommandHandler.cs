using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using SharedKernel;

namespace Application.Players.Register;

internal sealed class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand, int>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPlayerCommandHandler(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    public async Task<Result<int>> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        var playerEmail = Email.Create(request.Email);
        if (playerEmail.IsFailure)
        {
            return Result.Failure<int>(EmailErrors.InvalidFormat);
        }

        var authResponse = await _authenticationService.RegisterAsync(
            request.Email, request.Password);

        if (authResponse.IsFailure)
        {
            return Result.Failure<int>(authResponse.Error);
        }

        var player = new Player(
            playerEmail.Value,
            new Name(request.Name),
            authResponse.Value);

        _playerRepository.Add(player);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return player.Id;
    }
}
