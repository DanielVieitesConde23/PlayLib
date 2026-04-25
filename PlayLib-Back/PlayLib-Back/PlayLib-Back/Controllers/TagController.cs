using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase {
    private readonly ITagService _tagService;

    public TagController(ITagService tagService) {
        _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var tags = await _tagService.GetAllTags();
        return Ok(tags);
    }
}
