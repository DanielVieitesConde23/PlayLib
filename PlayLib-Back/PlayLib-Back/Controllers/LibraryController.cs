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
    public IActionResult AddToLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        try
        {
            _libraryService.AddToLibrary(videogameLibraryDTO);
            return Ok("Videojuego agregado a la biblioteca correctamente.");
        }
        catch (Exception ex) 
        { 
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/videogame")]
    public IActionResult RemoveFromLibrary(VideogameLibraryDTO videogameLibraryDTO)
    {
        try
        {
            _libraryService.RemoveFromLibrary(videogameLibraryDTO);
            return Ok("Videojuego eliminado de la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpPost]
    [Route("add/boardgame")]
    public IActionResult AddToLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        try
        {
            _libraryService.AddToLibrary(boardgameLibraryDTO);
            return Ok("Juego de mesa agregado a la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/boardgame")]
    public IActionResult RemoveFromLibrary(BoardgameLibraryDTO boardgameLibraryDTO)
    {
        try
        {
            _libraryService.RemoveFromLibrary(boardgameLibraryDTO);
            return Ok("Juego de mesa eliminado de la biblioteca correctamente.");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpGet]
    [Route("videogames/{userId}")]
    public IActionResult GetVideogamesByUserId(Guid userId)
    {
        try
        {
            var videogames = _libraryService.GetLibraryVideogamesForUser(userId);
            return Ok(videogames);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpGet]
    [Route("boardgames/{userId}")]
    public IActionResult GetBoardgamesByUserId(Guid userId)
    {
        try
        {
            var boardgames = _libraryService.GetLibraryBoardGamesForUser(userId);
            return Ok(boardgames);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}