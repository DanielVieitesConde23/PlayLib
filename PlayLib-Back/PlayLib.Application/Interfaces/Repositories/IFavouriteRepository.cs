using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface IFavouriteRepository
{
    Task<int> CountFavouriteVideogames(Guid userId);
    Task<int> CountFavouriteTabletops(Guid userId);

    Task<bool> AddVideogameFavourite(FavouriteVideogame fav);
    Task<bool> AddTabletopFavourite(FavouriteTabletop fav);

    Task<bool> RemoveVideogameFavourite(Guid userId, Guid videogameId);
    Task<bool> RemoveTabletopFavourite(Guid userId, Guid tabletopId);

    Task<IEnumerable<Videogame>> GetFavouriteVideogames(Guid userId);
    Task<IEnumerable<TabletopGame>> GetFavouriteTabletops(Guid userId);
}
