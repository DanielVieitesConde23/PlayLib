using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface ILibraryService {
    Task AddToLibrary(VideogameLibraryDTO videogameLibraryDTO);

    Task AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO);

    Task RemoveFromLibrary(VideogameLibraryDTO videogameLibraryDTO);

    Task RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO);

    Task<List<VideogameCarrousel>> GetLibraryVideogamesForUser(Guid userId);

    Task<List<VideogameCarrousel>> GetLibraryBoardGamesForUser(Guid userId);

}
