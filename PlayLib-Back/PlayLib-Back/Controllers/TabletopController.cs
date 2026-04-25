using Azure;
using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

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
            return NotFound(new { Success = false, Message = "Tabletop not found." });
        else 
            return Ok(await _tabletopService.GetTabletopWithReviews(tabletopId, userId));
    }

    [HttpGet]
    [Route("tabletop/GetTabletopByTag")]
    public async Task<IActionResult> GetTabletopsByTag(Guid userId) 
    {
        var tag = await _tabletopService.GetMostPupularTagForUser(userId);
        if (string.IsNullOrEmpty(tag))
            return NotFound(new { Success = false, Message = "No tags found for the user." });
        var tabletops = await _tabletopService.GetTabletopsByTag(tag, userId);
        if (tabletops == null || !tabletops.Any())
            return NotFound(new { Success = false, Message = "No tabletops found with the specified tag." });
        else
            return Ok(tabletops);
    }

    [HttpGet]
    [Route("tabletop/GetPopularTabletops")]
    public async Task<IActionResult> GetMostPupoularTabletops(Guid userId)
    {
        var tabletops = await _tabletopService.GetMostPopularTabletops(userId);
        if (tabletops == null || !tabletops.Any())
            return NotFound(new { Success = false, Message = "No tabletops found." });
        else
            return Ok(tabletops);
    }
}
