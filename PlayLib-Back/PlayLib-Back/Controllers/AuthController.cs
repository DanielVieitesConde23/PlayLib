using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;
using PlayLib.Data.Responses;

namespace PlayLib_Back.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IUserRepository userRepository, IPasswordHasher passwordHasher) : Controller
{

    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        if (userRegister.Password != userRegister.RepeatPassword)
        {
            return Conflict(new Response { Success = false, Message = "Las contraseñas no son iguales" });
        }

        if (await _userRepository.GetByEmail(userRegister.Email) != null)
        {
            return Conflict(new Response { Success = false, Message = "Este correo electrónico ya está registrado" });
        }

        if (await _userRepository.GetByUsername(userRegister.UserName) != null)
        {
            return Conflict(new Response { Success = false, Message = "Este nombre de usuario ya está registrado" });
        }

        var succes = await _authService.Register(userRegister);
        if (succes)
            return Ok(new Response { Success = true, Message = "Registro exitoso" });
        else
            return BadRequest(new Response { Success = false, Message = "Error en el registro. Inténtalo de nuevo." });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        User user = await _userRepository.GetByLoginInfo(userLogin.LoginInfo);

        if (user == null)
            return Ok(new Response { Success = false, Message = "Detalles de inicio de sesión incorrectos. Inténtalo de nuevo." });

        if (!_authService.IsPasswordCorrect(userLogin.Password, user.Password))
            return Ok(new Response { Success = false, Message = "Detalles de inicio de sesión incorrectos. Inténtalo de nuevo." });

        var authResponse = await _authService.Login(user);
        return Ok(authResponse);
    }

    private IActionResult BadRequestModelState()
    {
        IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        return BadRequest(new Response { Success = false, Message = errorMessages?.FirstOrDefault() });
    }

    [HttpPut]
    [Route("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(Guid userId, [FromBody] UpdatePasswordDto updatePasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }
        User user = await _userRepository.GetById(userId);
        if (user == null)
            return NotFound(new Response { Success = false, Message = "Usuario no encontrado." });
        if (!_authService.IsPasswordCorrect(updatePasswordDto.CurrentPassword, user.Password))
            return BadRequest(new Response { Success = false, Message = "Contraseña actual incorrecta. Inténtalo de nuevo." });
        if (updatePasswordDto.NewPassword != updatePasswordDto.RepeatNewPassword)
            return Conflict(new Response { Success = false, Message = "Las nuevas contraseñas no son iguales" });
        string newHashedPassword = _passwordHasher.HashPassword(updatePasswordDto.NewPassword);
        user.Password = newHashedPassword;
        await _userRepository.UpdateUser(user);
        return Ok(new Response { Success = true, Message = "Contraseña actualizada correctamente." });
    }

    [HttpPut]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword(string email)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        var user = await _userRepository.GetByEmail(email);

        if (user == null)
            return NotFound(new Response { Success = false, Message = "Usuario no encontrado." });

        if (_authService.ResetPassword(user))
            return Ok(new Response { Success = true, Message = "Contraseña restablecida correctamente. Revisa tu correo electrónico para obtenerla." });
        else
            return BadRequest(new Response { Success = false, Message = "Error al restablecer la contraseña. Inténtalo de nuevo." });
    }
}