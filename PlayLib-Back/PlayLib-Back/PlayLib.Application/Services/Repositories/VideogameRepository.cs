using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class VideogameRepository(PlayLibDContext context) : IVideogameRepository {

    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<bool> VideogameExists(Guid videogameId)
    {
        return await _dbContext.Videogames
            .AnyAsync(x => x.Id == videogameId);
    }

    public async Task<Videogame> GetVideogame(Guid videogameId)
    {
        return await _dbContext.Videogames
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
            .FirstOrDefaultAsync(x => x.Id == videogameId);
    }

    public async Task<IEnumerable<Videogame>> GetVideogamesByTag(string tag, Guid userId)
    {
        var normalizedTag = tag.ToLower();

        return await _dbContext.Videogames
                .Where(v =>
                    v.Tags.Any(t => t.Tag.Name.ToLower() == normalizedTag) &&
                    !v.Libraries.Any(l => l.UserId == userId)
                )
                .OrderByDescending(v => v.Libraries.Count())
                .Take(10)
                .ToListAsync();
    }

    public async Task<IEnumerable<Videogame>> GetMostPopularGames(Guid userId)
    {
        return await _dbContext.Videogames
                .Where(v =>
                    !v.Libraries.Any(l => l.UserId == userId)
                )
                .OrderByDescending(v => v.Libraries.Count())
                .Take(10)
                .ToListAsync();
    }

    public async Task<string> GetMostPupularTagForUser(Guid userId)
    {
        var mostPopularTag = await _dbContext.VideogameLibraries
            .Where(l => l.UserId == userId)
            .SelectMany(l => l.Videogame.Tags)
            .GroupBy(t => t.Tag.Name)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();

        return mostPopularTag ?? string.Empty;
    }
}
