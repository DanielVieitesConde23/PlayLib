using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class FavouriteRepository(PlayLibDContext context) : IFavouriteRepository
{
    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> CountFavouriteVideogames(Guid userId)
    {
        return await _dbContext.FavouriteVideogames.CountAsync(f => f.UserId == userId);
    }

    public async Task<int> CountFavouriteTabletops(Guid userId)
    {
        return await _dbContext.FavouriteTabletops.CountAsync(f => f.UserId == userId);
    }

    public async Task<bool> AddVideogameFavourite(FavouriteVideogame fav)
    {
        _dbContext.FavouriteVideogames.Add(fav);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddTabletopFavourite(FavouriteTabletop fav)
    {
        _dbContext.FavouriteTabletops.Add(fav);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveVideogameFavourite(Guid userId, Guid videogameId)
    {
        var fav = await _dbContext.FavouriteVideogames.FindAsync(userId, videogameId);
        if (fav == null) return false;
        _dbContext.FavouriteVideogames.Remove(fav);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveTabletopFavourite(Guid userId, Guid tabletopId)
    {
        var fav = await _dbContext.FavouriteTabletops.FindAsync(userId, tabletopId);
        if (fav == null) return false;
        _dbContext.FavouriteTabletops.Remove(fav);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Videogame>> GetFavouriteVideogames(Guid userId)
    {
        return await _dbContext.FavouriteVideogames
            .Where(f => f.UserId == userId)
            .Select(f => f.Videogame)
            .ToListAsync();
    }

    public async Task<IEnumerable<TabletopGame>> GetFavouriteTabletops(Guid userId)
    {
        return await _dbContext.FavouriteTabletops
            .Where(f => f.UserId == userId)
            .Select(f => f.TabletopGame)
            .ToListAsync();
    }
}
