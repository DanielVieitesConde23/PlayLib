using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface IVideogameService {

    Task<bool> VideogameExists(Guid videogameId);

    Task<VideogameWithReviews> GetVideogameWithReviews(Guid videogameId, Guid userId);

    Task<IEnumerable<VideogameCarrousel>> GetVideogamesByTag(string tag, Guid userId);

    Task<IEnumerable<VideogameCarrousel>> GetMostPopularGames(Guid userId);

    Task<string> GetMostPupularTagForUser(Guid userId);
}
