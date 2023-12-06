using SharedKernel;

namespace Domain.PlayerScores;

public sealed class PlayerScore : Entity
{
    public PlayerScore(int playerId, int levelId, float score)
    {
        PlayerId = playerId;
        LevelId = levelId;
        Score = score;
    }

    //Needed for EF Core
    public PlayerScore()
    {
    }

    public int PlayerId { get; private set; }
    
    public int LevelId { get; private set; }
    public float Score { get; private set; }

    public void Update(float score)
    {
        Score = score;
    }
}
