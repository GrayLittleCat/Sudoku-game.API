using Domain.Players;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Players;

internal sealed class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public PlayerRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
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

    public void Delete(Player player)
    {
        _context.Players.Remove(player);
    }

    public async Task<Player?> GetCurrentPlayerAsync()
    {
        var currentIdentityId = _httpContext.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == CustomClaims.UserId)?.Value;
        if (currentIdentityId is null)
        {
            return null;
        }

        return await _context.Players
            .SingleOrDefaultAsync(p => p.IdentityId == currentIdentityId);
    }

    public async Task<bool> IsCurrentPlayerAsync(int id)
    {
        var currentPlayer = await GetCurrentPlayerAsync();
        return currentPlayer?.Id == id;
    }
}
