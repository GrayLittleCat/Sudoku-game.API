using SharedKernel;

namespace Domain.PlayerScores;

public sealed class PlayerScore : Entity
{
    public int PlayerId { get; private set; } 
}
