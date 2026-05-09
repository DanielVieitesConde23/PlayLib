using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces.Repositories;

public interface IVideogameRepository {

    Task<bool> VideogameExists(Guid videogameId);

    Task<Videogame> GetVideogame(Guid videogameId);

    Task<IEnumerable<Videogame>> GetVideogamesByTag(string tag, Guid userId);

    Task<IEnumerable<Videogame>> GetMostPopularGames(Guid userId);

    Task<string> GetMostPupularTagForUser(Guid userId);

    Task<List<GameSearchResult>> SearchGamesByName(string name);

    Task UpdateLibraryState(Guid videogameId, Guid userId, string newState);

    Task UpdateLibraryFormat(Guid videogameId, Guid userId, string newFormat);

    Task CreateVideogame(Videogame videogame);
}
