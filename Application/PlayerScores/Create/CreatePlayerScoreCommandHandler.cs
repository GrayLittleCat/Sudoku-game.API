using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PlayerScores;
using SharedKernel;

namespace Application.PlayerScores.Create;

internal sealed class CreatePlayerScoreCommandHandler : ICommandHandler<CreatePlayerScoreCommand>
{
    private readonly IPlayerScoreRepository _playerScoreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlayerScoreCommandHandler(IUnitOfWork unitOfWork, IPlayerScoreRepository playerScoreRepository)
    {
        _unitOfWork = unitOfWork;
        _playerScoreRepository = playerScoreRepository;
    }

    public async Task<Result> Handle(CreatePlayerScoreCommand request, CancellationToken cancellationToken)
    {
        var playerScore = new PlayerScore(request.PlayerId, request.LevelId, request.Score);

        _playerScoreRepository.Insert(playerScore);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
