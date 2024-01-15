using Domain.Players;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(int playerId)
    {
        var roles = await _context.Set<Player>()
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == playerId)
            .Select(x => x.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
