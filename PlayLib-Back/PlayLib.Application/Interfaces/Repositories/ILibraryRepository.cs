using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces.Repositories;

public interface ILibraryRepository {

    Task AddToLibrary(VideogameLibraryDTO videogameLibraryDTO);

    Task RemoveFromLibrary(VideogameLibraryDTO libraryDTO);

    Task<List<VideogameLibrary>> GetLibraryVideogameForUser(Guid userId);

    Task AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO);

    Task RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO);

    Task<List<TabletopLibrary>> GetLibraryBoardGameForUser(Guid userId);
}
