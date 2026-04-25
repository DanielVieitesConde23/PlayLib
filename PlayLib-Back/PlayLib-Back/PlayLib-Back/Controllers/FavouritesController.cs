using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritesController(IFavouriteService favouriteService) : ControllerBase
{
    private readonly IFavouriteService _favouriteService = favouriteService ?? throw new ArgumentNullException(nameof(favouriteService));

    [HttpPost]
    [Route("videogame/{userId}/{videogameId}")]
    public async Task<IActionResult> AddVideogameFavourite(Guid userId, Guid videogameId)
    {
        var added = await _favouriteService.AddVideogameFavourite(userId, videogameId);
        if (!added)
            return BadRequest(new { Success = false, Message = "Could not add favourite (limit 10 or already exists)." });
        return Ok(new { Success = true });
    }

    [HttpPost]
    [Route("tabletop/{userId}/{tabletopId}")]
    public async Task<IActionResult> AddTabletopFavourite(Guid userId, Guid tabletopId)
    {
        var added = await _favouriteService.AddTabletopFavourite(userId, tabletopId);
        if (!added)
            return BadRequest(new { Success = false, Message = "Could not add favourite (limit 10 or already exists)." });
        return Ok(new { Success = true });
    }

    [HttpDelete]
    [Route("videogame/{userId}/{videogameId}")]
    public async Task<IActionResult> RemoveVideogameFavourite(Guid userId, Guid videogameId)
    {
        var removed = await _favouriteService.RemoveVideogameFavourite(userId, videogameId);
        if (!removed)
            return NotFound(new { Success = false, Message = "Favourite not found." });
        return Ok(new { Success = true });
    }

    [HttpDelete]
    [Route("tabletop/{userId}/{tabletopId}")]
    public async Task<IActionResult> RemoveTabletopFavourite(Guid userId, Guid tabletopId)
    {
        var removed = await _favouriteService.RemoveTabletopFavourite(userId, tabletopId);
        if (!removed)
            return NotFound(new { Success = false, Message = "Favourite not found." });
        return Ok(new { Success = true });
    }

    [HttpGet]
    [Route("videogame/{userId}")]
    public async Task<IActionResult> GetFavouriteVideogames(Guid userId)
    {
        var favs = await _favouriteService.GetFavouriteVideogames(userId);
        return Ok(favs);
    }

    [HttpGet]
    [Route("tabletop/{userId}")]
    public async Task<IActionResult> GetFavouriteTabletops(Guid userId)
    {
        var favs = await _favouriteService.GetFavouriteTabletops(userId);
        return Ok(favs);
    }
}
