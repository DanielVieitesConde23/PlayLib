using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface IUserRepository {
    Task<User> GetByEmail(string email);

    Task<User> GetByUsername(string username);

    Task<User> GetByLoginInfo(string loginInfo);

    Task<User> GetById(Guid id);

    Task<bool> Create(User user);
}