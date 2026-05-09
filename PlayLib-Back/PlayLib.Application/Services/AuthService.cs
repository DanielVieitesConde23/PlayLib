using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;
using PlayLib.Data.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PlayLib.Application.Services;

public class AuthService(IPasswordHasher passwordHasher, AuthConfiguration authConfiguration, IUserRepository userRepository, IEmailSender emailSender) : IAuthService {

    private readonly IPasswordHasher _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly AuthConfiguration _authConfiguration = authConfiguration ?? throw new ArgumentNullException(nameof(authConfiguration));
    private readonly IEmailSender _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));

    public async Task<AuthUserResponse> Login(User user)
    {

        var accessToken = GenerateToken(user);
        return new AuthUserResponse
        {
            AccessToken = accessToken,
            UserId = user.Id,
            Success = true,
            Message = "Login success"
        };
    }

    public async Task<bool> Register(UserRegisterDto userRegister)
    {
        string passwordHashed = _passwordHasher.HashPassword(userRegister.Password);

        User user = new User
        {
            Email = userRegister.Email,
            Password = passwordHashed,
            UserName = userRegister.UserName,
            Role = "User"
        };

        await _userRepository.Create(user);

        return true;
    }

    public bool IsPasswordCorrect(string password, string passwordHash)
    {
        return _passwordHasher.VerifyPassword(password, passwordHash);
    }

    public bool ResetPassword(User user)
    {
        try
        {
            var newPassword = GeneratePassword();
            string newHashedPassword = _passwordHasher.HashPassword(newPassword);
            user.Password = newHashedPassword;
            _userRepository.UpdateUser(user);
            _emailSender.SendEmail(user.Email, "Restablecimiento de contraseña", $"Tu nueva contraseña es: {newPassword}. Si no realizaste esta acción, por favor restablece tu contraseña.");
            return true;
        } catch (Exception)
        {
            return false;
        }


    }

    private static string GeneratePassword(int length = 12)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(
            [.. Enumerable.Range(0, length).Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)])]);
    }

    private string GenerateToken(User user)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Key));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>()
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        JwtSecurityToken token = new JwtSecurityToken(
            _authConfiguration.Issuer,
            _authConfiguration.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_authConfiguration.ExpirationMinutes),
            credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
