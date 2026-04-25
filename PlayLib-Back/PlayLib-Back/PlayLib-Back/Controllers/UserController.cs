using Microsoft.AspNetCore.Mvc;
using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;
using PlayLib.Data.Responses;

namespace PlayLib_Back.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {

    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AuthController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        if (userRegister.Password != userRegister.RepeatPassword)
        {
            return Conflict(new Response { Success = false, Message = "The passwords are not the same" });
        }

        if (await _userRepository.GetByEmail(userRegister.Email) != null)
        {
            return Conflict(new Response { Success = false, Message = "This email is already registered" });
        }

        if (await _userRepository.GetByUsername(userRegister.UserName) != null)
        {
            return Conflict(new Response { Success = false, Message = "This username is already registered" });
        }

        var succes = await _authService.Register(userRegister);
        if (succes)
            return Ok(new Response { Success = true, Message = "Register Succes" });
        else
            return BadRequest(new Response { Success = false, Message = "Register Failed. Try Again." });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequestModelState();
        }

        User user = await _userRepository.GetByLoginInfo(userLogin.LoginInfo);

        if (user == null)
            return Ok(new Response { Success = false, Message = "Incorrect login details. Please try again." });

        if (!_authService.IsPasswordCorrect(userLogin.Password, user.Password))
            return Ok(new Response { Success = false, Message = "Incorrect login details. Please try again." });

        var authResponse = await _authService.Login(user);
        return Ok(authResponse);
    }

    private IActionResult BadRequestModelState()
    {
        IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        return BadRequest(new Response { Success = false, Message = errorMessages?.FirstOrDefault() });
    }
}
