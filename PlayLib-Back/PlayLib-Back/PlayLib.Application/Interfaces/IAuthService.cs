using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;
using PlayLib.Data.Responses;

namespace PlayLib.Application.Interfaces;
public interface IAuthService {
    Task<bool> Register(UserRegisterDto userRegisterDto);

    Task<AuthUserResponse> Login(User user);

    bool IsPasswordCorrect(string password, string passwordHash);
}
