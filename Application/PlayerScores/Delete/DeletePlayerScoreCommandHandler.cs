using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.Delete;

internal sealed class DeletePlayerScoreCommandHandler : ICommandHandler<DeletePlayerScoreCommand>
{
    private readonly IPlayerScoreRepository _playerScoreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePlayerScoreCommandHandler(IPlayerScoreRepository playerScoreRepository, IUnitOfWork unitOfWork)
    {
        _playerScoreRepository = playerScoreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePlayerScoreCommand request, CancellationToken cancellationToken)
    {
        var playerScore = await _playerScoreRepository.GetByIdAsync(request.PlayerScoreId);
        if (playerScore == null)
        {
            return Result.Failure(PlayerScoreErrors.NotFound(request.PlayerScoreId));
        }

        _playerScoreRepository.Delete(playerScore);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
