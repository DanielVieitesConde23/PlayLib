
using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideogameController(IVideogameService videogameService) : ControllerBase {

    private readonly IVideogameService _videogameService = videogameService ?? throw new ArgumentNullException(nameof(videogameService));

    [HttpGet]
    [Route("videogame/{videogameId}")]
    public async Task<IActionResult> GetVideogameWithReviews(Guid videogameId, Guid userId) 
    {
        if (!await _videogameService.VideogameExists(videogameId))
            return NotFound(new { Success = false, Message = "Videogame not found." });
        else 
            return Ok(await _videogameService.GetVideogameWithReviews(videogameId, userId));
    }

    [HttpGet]
    [Route("videogame/GetVideogameByTag")]
    public async Task<IActionResult> GetVideogamesByTag(Guid userId) 
    {
        var tag = await _videogameService.GetMostPupularTagForUser(userId);
        if (string.IsNullOrEmpty(tag))
            return NotFound(new { Success = false, Message = "No tags found for the user." });
        var videogames = await _videogameService.GetVideogamesByTag(tag, userId);
        if (videogames == null || !videogames.Any())
            return NotFound(new { Success = false, Message = "No videogames found with the specified tag." });
        else
            return Ok(videogames);
    }

    [HttpGet]
    [Route("videogame/GetPopularGames")]
    public async Task<IActionResult> GetMostPupoularGames(Guid userId)
    {
        var videogames = await _videogameService.GetMostPopularGames(userId);
        if (videogames == null || !videogames.Any())
            return NotFound(new { Success = false, Message = "No videogames found." });
        else
            return Ok(videogames);
    }
}
