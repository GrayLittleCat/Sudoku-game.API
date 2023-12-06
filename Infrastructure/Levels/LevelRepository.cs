using Domain.Levels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Levels;

internal sealed class LevelRepository : ILevelRepository
{
    private readonly ApplicationDbContext _context;

    public LevelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Level?> GetByIdAsync(int id)
    {
        return _context.Levels.SingleOrDefaultAsync(l => l.Id == id);
    }

    public void Insert(Level level)
    {
        _context.Levels.Add(level);
    }

    public void Update(Level level)
    {
        _context.Levels.Update(level);
    }

    public void Delete(Level level)
    {
        _context.Levels.Remove(level);
    }
}
