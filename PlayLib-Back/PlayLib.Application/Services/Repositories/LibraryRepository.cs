using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class LibraryRepository(PlayLibDContext playLibDContext) : ILibraryRepository {

    private readonly PlayLibDContext _playlibContext = playLibDContext;

    public async Task AddToLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        _playlibContext.VideogameLibraries.Add(new VideogameLibrary
        {
            Id = Guid.NewGuid(),
            UserId = videogameLibraryDTO.UserId,
            VideogameId = videogameLibraryDTO.VideogameId
        });
        await _playlibContext.SaveChangesAsync();
    }

    public async Task RemoveFromLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        var videogameLibrary = _playlibContext.VideogameLibraries.FirstOrDefault(x => x.UserId == videogameLibraryDTO.UserId && x.VideogameId == videogameLibraryDTO.VideogameId);
        if (videogameLibrary != null)
        {
            _playlibContext.VideogameLibraries.Remove(videogameLibrary);
            await _playlibContext.SaveChangesAsync();
        }
    }

    public async Task<List<VideogameLibrary>> GetLibraryVideogameForUser(Guid userId)
    {
        return await _playlibContext.VideogameLibraries
                .Where(x => x.UserId == userId)
                .Include(x => x.Videogame)
                .ToListAsync();
    }

    public async Task AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        _playlibContext.TabletopLibraries.Add(new TabletopLibrary
        {
            Id = Guid.NewGuid(),
            UserId = boardgameLibraryDTO.UserId,
            TabletopId = boardgameLibraryDTO.BoardgameId
        });
        await _playlibContext.SaveChangesAsync();
    }

    public async Task RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        var boardgameLibrary = _playlibContext.TabletopLibraries.FirstOrDefault(x => x.UserId == boardgameLibraryDTO.UserId && x.TabletopId == boardgameLibraryDTO.BoardgameId);
        if (boardgameLibrary != null)
        {
            _playlibContext.TabletopLibraries.Remove(boardgameLibrary);
            await _playlibContext.SaveChangesAsync();
        }
    }

    public async Task<List<TabletopLibrary>> GetLibraryBoardGameForUser(Guid userId)
    {
        return await _playlibContext.TabletopLibraries
                .Where(x => x.UserId == userId)
                .Include(x => x.Tabletop)
                .ToListAsync();
    }
}
    