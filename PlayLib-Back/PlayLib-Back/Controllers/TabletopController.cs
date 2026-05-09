using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TabletopController(ITabletopService tabletopService) : ControllerBase {

    private readonly ITabletopService _tabletopService = tabletopService ?? throw new ArgumentNullException(nameof(tabletopService));

    [HttpGet]
    [Route("tabletop/{tabletopId}")]
    public async Task<IActionResult> GetTabletopWithReviews(Guid tabletopId, Guid userId)
    {
        if (!await _tabletopService.TabletopExists(tabletopId))
            return NotFound(new { Success = false, Message = "Juego de mesa no encontrado." });
        else
            return Ok(await _tabletopService.GetTabletopWithReviews(tabletopId, userId));
    }

    [HttpGet]
    [Route("tabletop/GetTabletopByTag")]
    public async Task<IActionResult> GetTabletopsByTag(Guid userId)
    {
        var tag = await _tabletopService.GetMostPupularTagForUser(userId);
        if (string.IsNullOrEmpty(tag))
            return NotFound(new { Success = false, Message = "No se encontró ninguna etiqueta popular para el usuario." });
        var tabletops = await _tabletopService.GetTabletopsByTag(tag, userId);
        if (tabletops == null || !tabletops.Any())
            return NotFound(new { Success = false, Message = "No se encontraron juegos de mesa con la etiqueta especificada." });
        else
            return Ok(tabletops);
    }

    [HttpGet]
    [Route("tabletop/GetPopularTabletops")]
    public async Task<IActionResult> GetMostPupoularTabletops(Guid userId)
    {
        var tabletops = await _tabletopService.GetMostPopularTabletops(userId);
        if (tabletops == null || !tabletops.Any())
            return NotFound(new { Success = false, Message = "No se encontraron juegos de mesa populares." });
        else
            return Ok(tabletops);
    }

    [HttpPut]
    [Route("tabletop/UpdateTabletopPlayedGames")]
    public async Task<IActionResult> UpdateTabletopPlayedGames(Guid tabletopId, Guid userId, int playedGames)
    {
        if (!await _tabletopService.TabletopExists(tabletopId))
            return NotFound(new { Success = false, Message = "Juego de mesa no encontrado." });
        else
        {
            var result = await _tabletopService.UpdateTabletopPlayedGames(tabletopId, userId, playedGames);
            if (!result)
                return BadRequest(new { Success = false, Message = "No se pudo actualizar el estado del juego de mesa." });
            else
                return Ok(new { Success = true, Message = "Estado del juego de mesa actualizado correctamente." });
        }
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTabletopGame([FromBody] CreateTabletopGameDTO tabletopGameDTO)
    {
        if (tabletopGameDTO == null)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "Datos del juego de mesa inválidos."
            });
        }

        var result = await _tabletopService.CreateTabletopGame(tabletopGameDTO);

        if (!result)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "No se pudo crear el juego de mesa."
            });
        }

        return Ok(new
        {
            Success = true,
            Message = "Juego de mesa creado correctamente."
        });
    }
}
