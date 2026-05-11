using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController(ILibraryService libraryService) : Controller {

    private readonly ILibraryService _libraryService = libraryService;

    [HttpPost]
    [Route("add/videogame")]
    public async Task<IActionResult> AddToLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        try
        {
            await _libraryService.AddToLibrary(videogameLibraryDTO);
            return Ok("Videojuego agregado a la biblioteca correctamente.");
        }
        catch (Exception ex) 
        { 
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/videogame")]
    public async Task<IActionResult> RemoveFromLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        try
        {
            await _libraryService.RemoveFromLibrary(videogameLibraryDTO);
            return Ok("Videojuego eliminado de la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpPost]
    [Route("add/tabletop")]
    public async Task<IActionResult> AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        try
        {
            await _libraryService.AddToLibrary(boardgameLibraryDTO);
            return Ok("Juego de mesa agregado a la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/tabletop")]
    public async Task<IActionResult> RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        try
        {
            await _libraryService.RemoveFromLibrary(boardgameLibraryDTO);
            return Ok("Juego de mesa eliminado de la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpGet]
    [Route("videogames/{userId}")]
    public async Task<IActionResult> GetVideogamesByUserId(Guid userId)
    {
        try
        {
            var videogames = await _libraryService.GetLibraryVideogamesForUser(userId);
            return Ok(videogames);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpGet]
    [Route("tabletop/{userId}")]
    public async Task<IActionResult> GetBoardgamesByUserId(Guid userId)
    {
        try
        {
            var boardgames = await _libraryService.GetLibraryBoardGamesForUser(userId);
            return Ok(boardgames);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}