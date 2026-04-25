using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class UserRepository(PlayLibDContext context) : IUserRepository {

    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<bool> Create(User user)
    {
        try
        {
            await _dbContext.Users.AddAsync(user);

            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<User> GetByLoginInfo(string loginInfo)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == loginInfo || x.Email == loginInfo);
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }
}