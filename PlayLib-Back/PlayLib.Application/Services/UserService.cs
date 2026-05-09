using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;

namespace PlayLib.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService {

    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserProfileDTO> GetUserProfile(Guid userId)
    {
        return await _userRepository.GetUserProfile(userId);
    }

    public async Task<bool> UpdateImage(Guid userId, string imageUrl)
    {
        return await _userRepository.UpdateImage(userId, imageUrl);
    }

    public async Task<bool> UpdateUsername(Guid userId, string username)
    {
        var user = await _userRepository.GetByUsername(username);
        if (user == null)
        {
            return false;
        }
        user.UserName = username;
        return await _userRepository.UpdateUser(user);
    }
    public async Task<bool> DeleteUser(Guid userId)
    {
        return await _userRepository.DeleteUser(userId);
    }

}
