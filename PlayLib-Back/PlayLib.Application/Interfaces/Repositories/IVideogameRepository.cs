namespace PlayLib.Application.Interfaces.Repositories;

public interface IVideogameRepository {

    Task<bool> VideogameExists(Guid videogameId);

    Task<Videogame> GetVideogame(Guid videogameId);

    Task<IEnumerable<Videogame>> GetVideogamesByTag(string tag, Guid userId);

    Task<IEnumerable<Videogame>> GetMostPopularGames(Guid userId);

    Task<string> GetMostPupularTagForUser(Guid userId);
}
