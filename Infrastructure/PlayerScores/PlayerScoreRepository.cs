using Domain.PlayerScores;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.PlayerScores;

internal sealed class PlayerScoreRepository : IPlayerScoreRepository
{
    private readonly ApplicationDbContext _context;

    public PlayerScoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<PlayerScore?> GetByIdAsync(int id)
    {
        return _context.PlayerScores.SingleOrDefaultAsync(ps => ps.Id == id);
    }

    public void Insert(PlayerScore playerScore)
    {
        _context.PlayerScores.Add(playerScore);
    }

    public void Update(PlayerScore playerScore)
    {
        _context.PlayerScores.Update(playerScore);
    }

    public void Delete(PlayerScore playerScore)
    {
        _context.PlayerScores.Remove(playerScore);
    }
}
