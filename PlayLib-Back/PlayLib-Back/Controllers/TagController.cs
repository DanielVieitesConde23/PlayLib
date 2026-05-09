using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController(ITagService tagService) : ControllerBase {
    private readonly ITagService _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll() {
        var tags = await _tagService.GetAllTags();
        return Ok(tags);
    }
}
