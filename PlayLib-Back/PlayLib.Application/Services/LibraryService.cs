using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;

namespace PlayLib.Application.Services;

public class LibraryService(ILibraryRepository libraryRepository) : ILibraryService {

    private readonly ILibraryRepository _libraryRepository = libraryRepository;

    public async Task AddToLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        await _libraryRepository.AddToLibrary(videogameLibraryDTO);
    }

    public async Task AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        await _libraryRepository.AddToLibrary(boardgameLibraryDTO);
    }

    public async Task RemoveFromLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        await _libraryRepository.RemoveFromLibrary(videogameLibraryDTO);
    }
    
    public async Task RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        await _libraryRepository.RemoveFromLibrary(boardgameLibraryDTO);
    }

    public async Task<List<VideogameCarrousel>> GetLibraryVideogamesForUser(Guid userId)
    {
        var videogames = await _libraryRepository.GetLibraryVideogameForUser(userId);

        return videogames.Select(v => new VideogameCarrousel
        {
            Id = v.VideogameId,
            Name = v.Videogame.Name,
            Image = v.Videogame.ImageRoute
        }).ToList();
    }

    public async Task<List<VideogameCarrousel>> GetLibraryBoardGamesForUser(Guid userId)
    {
        var boardgames = await _libraryRepository.GetLibraryBoardGameForUser(userId);

        return boardgames.Select(v => new VideogameCarrousel
        {
            Id = v.TabletopId,
            Name = v.Tabletop.Name,
            Image = v.Tabletop.ImageRoute
        }).ToList();
    }
}
