using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using SharedKernel;

namespace Application.Players.Delete;

internal sealed class DeletePlayerCommandHandler : ICommandHandler<DeletePlayerCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePlayerCommandHandler(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId);
        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        var result = await _authenticationService.DeleteAsync(player.IdentityId);
        if (result.IsFailure)
        {
            return result.Error.ToResult();
        }

        _playerRepository.Delete(player);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}
