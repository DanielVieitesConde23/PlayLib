using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface IUserService {
    Task<UserProfileDTO> GetUserProfile(Guid userId);

    Task<bool> UpdateUsername(Guid userId, string username);

    Task<bool> UpdateImage(Guid userId, string imageUrl);

    Task<bool> DeleteUser(Guid userId);
}
