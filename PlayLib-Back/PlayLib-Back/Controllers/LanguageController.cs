using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguageController(ILanguageService languageService) : ControllerBase {
    private readonly ILanguageService _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll() {
        var languages = await _languageService.GetAllLanguages();
        return Ok(languages);
    }
}
