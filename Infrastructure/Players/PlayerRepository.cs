using Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Players;

internal sealed class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _context;

    public PlayerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Player?> GetByIdAsync(int id)
    {
        return _context.Players
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _context.Players.AnyAsync(c => c.Email.Value == email);
    }

    public void Add(Player player)
    {
        _context.Players.Add(player);
    }
}
