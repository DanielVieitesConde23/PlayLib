using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface IFavouriteService
{
    Task<bool> AddVideogameFavourite(Guid userId, Guid videogameId);
    Task<bool> AddTabletopFavourite(Guid userId, Guid tabletopId);

    Task<bool> RemoveVideogameFavourite(Guid userId, Guid videogameId);
    Task<bool> RemoveTabletopFavourite(Guid userId, Guid tabletopId);

    Task<IEnumerable<VideogameCarrousel>> GetFavouriteVideogames(Guid userId);
    Task<IEnumerable<TabletopCarrousel>> GetFavouriteTabletops(Guid userId);
}
