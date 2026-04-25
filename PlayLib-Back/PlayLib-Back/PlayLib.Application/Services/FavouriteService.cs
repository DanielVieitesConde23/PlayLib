using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services;

public class FavouriteService(IFavouriteRepository favouriteRepository) : IFavouriteService
{
    private readonly IFavouriteRepository _favouriteRepository = favouriteRepository ?? throw new ArgumentNullException(nameof(favouriteRepository));

    public async Task<bool> AddVideogameFavourite(Guid userId, Guid videogameId)
    {
        var count = await _favouriteRepository.CountFavouriteVideogames(userId);
        if (count >= 10) return false;

        var added = await _favouriteRepository.AddVideogameFavourite(new FavouriteVideogame { UserId = userId, VideogameId = videogameId });
        return added;
    }

    public async Task<bool> AddTabletopFavourite(Guid userId, Guid tabletopId)
    {
        var count = await _favouriteRepository.CountFavouriteTabletops(userId);
        if (count >= 10) return false;

        var added = await _favouriteRepository.AddTabletopFavourite(new FavouriteTabletop { UserId = userId, TabletopId = tabletopId });
        return added;
    }

    public async Task<bool> RemoveVideogameFavourite(Guid userId, Guid videogameId)
    {
        return await _favouriteRepository.RemoveVideogameFavourite(userId, videogameId);
    }

    public async Task<bool> RemoveTabletopFavourite(Guid userId, Guid tabletopId)
    {
        return await _favouriteRepository.RemoveTabletopFavourite(userId, tabletopId);
    }

    public async Task<IEnumerable<VideogameCarrousel>> GetFavouriteVideogames(Guid userId)
    {
        var videogames = await _favouriteRepository.GetFavouriteVideogames(userId);
        if (videogames == null) return Enumerable.Empty<VideogameCarrousel>();

        return videogames.Select(v => new VideogameCarrousel { Id = v.Id, Name = v.Name, Image = v.ImageRoute }).ToList();
    }

    public async Task<IEnumerable<TabletopCarrousel>> GetFavouriteTabletops(Guid userId)
    {
        var tabletops = await _favouriteRepository.GetFavouriteTabletops(userId);
        if (tabletops == null) return Enumerable.Empty<TabletopCarrousel>();

        return tabletops.Select(t => new TabletopCarrousel { Id = t.Id, Name = t.Name, Image = t.ImageRoute }).ToList();
    }
}
