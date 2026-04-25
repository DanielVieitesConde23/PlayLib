using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class TabletopRepository(PlayLibDContext context) : ITabletopRepository {

    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<bool> TabletopExists(Guid tabletopId)
    {
        return await _dbContext.TabletopGames
            .AnyAsync(x => x.Id == tabletopId);
    }

    public async Task<TabletopGame> GetTabletop(Guid tabletopId)
    {
        return await _dbContext.TabletopGames
            .Include(v => v.Reviews)
                .ThenInclude(r => r.User)
            .Include(v => v.Tags)
                .ThenInclude(r => r.Tag)
             .Include(v => v.Languages)
                .ThenInclude(l => l.Language)
            .Include(v => v.Libraries)
                .ThenInclude(l => l.User)
            .Include(v => v.Favourites)
                .ThenInclude(f => f.User)
            .FirstOrDefaultAsync(x => x.Id == tabletopId);
    }

    public async Task<IEnumerable<TabletopGame>> GetTabletopsByTag(string tag, Guid userId)
    {
        var normalizedTag = tag.ToLower();

        return await _dbContext.TabletopGames
                .Where(v =>
                    v.Tags.Any(t => t.Tag.Name.ToLower() == normalizedTag) &&
                    !v.Libraries.Any(l => l.UserId == userId)
                )
                .OrderByDescending(v => v.Libraries.Count())
                .Take(10)
                .ToListAsync();
    }

    public async Task<IEnumerable<TabletopGame>> GetMostPopularTabletops(Guid userId)
    {
        return await _dbContext.TabletopGames
                .Where(v =>
                    !v.Libraries.Any(l => l.UserId == userId)
                )
                .OrderByDescending(v => v.Libraries.Count())
                .Take(10)
                .ToListAsync();
    }

    public async Task<string> GetMostPupularTagForUser(Guid userId)
    {
        var mostPopularTag = await _dbContext.TabletopLibraries
            .Where(l => l.UserId == userId)
            .SelectMany(l => l.TabletopGame.Tags)
            .GroupBy(t => t.Tag.Name)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();

        return mostPopularTag ?? string.Empty;
    }
}
