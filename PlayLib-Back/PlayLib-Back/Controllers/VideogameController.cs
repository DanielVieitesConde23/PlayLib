
using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideogameController(IVideogameService videogameService) : ControllerBase {

    private readonly IVideogameService _videogameService = videogameService ?? throw new ArgumentNullException(nameof(videogameService));

    [HttpGet]
    [Route("{videogameId}")]
    public async Task<IActionResult> GetVideogameWithReviews(Guid videogameId, Guid userId) 
    {
        if (!await _videogameService.VideogameExists(videogameId))
            return NotFound(new { Success = false, Message = "Videojuego no encontrado." });
        else 
            return Ok(await _videogameService.GetVideogameWithReviews(videogameId, userId));
    }

    [HttpGet]
    [Route("GetVideogameByTag")]
    public async Task<IActionResult> GetVideogamesByTag(Guid userId) 
    {
        var tag = await _videogameService.GetMostPupularTagForUser(userId);
        if (string.IsNullOrEmpty(tag))
            return NotFound(new { Success = false, Message = "No se encontró ninguna etiqueta popular para el usuario." });
        var videogames = await _videogameService.GetVideogamesByTag(tag, userId);
        if (videogames == null || !videogames.Any())
            return NotFound(new { Success = false, Message = "No se encontraron videojuegos con la etiqueta especificada." });
        else
            return Ok(videogames);
    }

    [HttpGet]
    [Route("GetPopularGames")]
    public async Task<IActionResult> GetMostPupoularGames(Guid userId)
    {
        var videogames = await _videogameService.GetMostPopularGames(userId);
        if (videogames == null || !videogames.Any())
            return NotFound(new { Success = false, Message = "No se encontraron videojuegos populares." });
        else
            return Ok(videogames);
    }

    [HttpGet]
    [Route("GetGamesbySearch/{name}")]
    public async Task<IActionResult> GetGamesBySearch(string name)
    {
        var games = await _videogameService.SearchGamesByName(name);
        if (games == null || !games.Any())
            return NotFound(new { Success = false, Message = "No se encontraron videojuegos con el nombre especificado." });
        else
            return Ok(games);
    }

    [HttpPut]
    [Route("UpdateState")]
    public async Task<IActionResult> UpdateVideogameState(Guid videogameId, Guid userId, string newState)
    {
        if (!await _videogameService.VideogameExists(videogameId))
            return NotFound(new { Success = false, Message = "Videojuego no encontrado." });
        var result = await _videogameService.UpdateVideogameState(videogameId, userId, newState);
        if (!result)
            return BadRequest(new { Success = false, Message = "No se pudo actualizar el estado del videojuego." });
        else
            return Ok(new { Success = true, Message = "Estado del videojuego actualizado correctamente." });
    }


    [HttpPut]
    [Route("UpdateFormat")]
    public async Task<IActionResult> UpdateVideogameFormat(Guid videogameId, Guid userId, string newFormat)
    {
        if (!await _videogameService.VideogameExists(videogameId))
            return NotFound(new { Success = false, Message = "Videojuego no encontrado." });
        var result = await _videogameService.UpdateVideogameFormat(videogameId, userId, newFormat);
        if (!result)
            return BadRequest(new { Success = false, Message = "No se pudo actualizar el formato del videojuego." });
        else
            return Ok(new { Success = true, Message = "Formato del videojuego actualizado correctamente." });
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateVideogame([FromBody] CreateVideogameDTO videogameDTO)
    {
        if (videogameDTO == null)
            return BadRequest(new
            {
                Success = false,
                Message = "Datos del videojuego inválidos."
            });

        var result = await _videogameService.CreateVideogame(videogameDTO);

        if (!result)
            return BadRequest(new
            {
                Success = false,
                Message = "No se pudo crear el videojuego."
            });

        return Ok(new
        {
            Success = true,
            Message = "Videojuego creado correctamente."
        });
    }
}
