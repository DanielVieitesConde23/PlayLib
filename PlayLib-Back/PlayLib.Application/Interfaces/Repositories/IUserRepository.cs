using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface IUserRepository {
    Task<User> GetByEmail(string email);

    Task<User> GetByUsername(string username);

    Task<User> GetByLoginInfo(string loginInfo);

    Task<User> GetById(Guid id);

    Task<bool> Create(User user);

    Task<string> GetUserEmailByRequest(Guid requestId);

    Task<bool> UpdateUser(User user);

    Task<UserProfileDTO> GetUserProfile(Guid userId);

    Task<bool> UpdateImage(Guid userId, string imageUrl);

    Task<bool> UpdateUsername(Guid userId, string username);

    Task<bool> DeleteUser(Guid userId);
}