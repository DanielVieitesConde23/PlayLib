using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;

namespace PlayLib_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IUserService userService) : Controller {

    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    [HttpGet]
    [Route("GetUserProfile/{userId}")]
    public async Task<IActionResult> GetUserProfile(Guid userId)
    {
        var userProfile = await _userService.GetUserProfile(userId);
        if (userProfile == null)
        {
            return NotFound();
        }
        return Ok(userProfile);
    }

    [HttpPut]
    [Route("UpdateUsername/{userId}/{username}")]
    public async Task<IActionResult> UpdateUsername(Guid userId, string username)
    {
        var success = await _userService.UpdateUsername(userId, username);
        if (!success)
        {
            return BadRequest(new { Success = false, Message = "El nombre de usuario ya existe." });
        }
        return Ok(new { Success = true });
    }

    [HttpPut]
    [Route("UpdateImage/{userId}")]
    public async Task<IActionResult> UpdateImage(Guid userId, [FromBody] string imageUrl)
    {
        var success = await _userService.UpdateImage(userId, imageUrl);
        if (!success)
        {
            return BadRequest(new { Success = false, Message = "No se pudo actualizar la imagen." });
        }
        return Ok(new { Success = true });
    }

    [HttpDelete]
    [Route("DeleteAccount/{userId}")]
    public async Task<IActionResult> DeleteAccount(Guid userId)
    {
        var success = await _userService.DeleteUser(userId);

        if (!success)
        {
            return NotFound(new
            {
                Success = false,
                Message = "Usuario no encontrado."
            });
        }

        return Ok(new
        {
            Success = true,
            Message = "Cuenta eliminada correctamente."
        });
    }
}