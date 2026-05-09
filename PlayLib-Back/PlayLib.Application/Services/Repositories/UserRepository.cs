using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
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
        return await _dbContext.Users
            .FirstAsync(x => x.Email == email);
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _dbContext.Users
            .FirstAsync(x => x.UserName == username);
    }

    public async Task<User> GetByLoginInfo(string loginInfo)
    {
        return await _dbContext.Users
            .FirstAsync(x =>
                x.UserName == loginInfo ||
                x.Email == loginInfo);
    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        return user is null ? throw new Exception("User not found") : user;
    }

    public async Task<string> GetUserEmailByRequest(Guid requestId)
    {
        var email = await _dbContext.Users
                .Where(u => u.Requests.Any(r => r.Id == requestId))
                .Select(u => u.Email)
                .FirstOrDefaultAsync();

        return email is null ? throw new Exception("Request user not found") : email;
    }

    public async Task<bool> UpdateUser(User user)
    {
        try
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<UserProfileDTO> GetUserProfile(Guid userId) 
    {
        var profile = await _dbContext.Users
                    .Where(u => u.Id == userId)
                    .Select(u => new UserProfileDTO
                    {
                        UserName = u.UserName,
                        Image_Route = u.Profile_Img,
                        Total_Videogames = u.VideogameLibrary.Count(),
                        Total_Tabletop_Games = u.TabletopLibrary.Count()
                    })
                    .FirstOrDefaultAsync();

        return profile is null ? throw new Exception("User not found") : profile;
    }

    public async Task<bool> UpdateImage(Guid userId, string imageUrl)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.Profile_Img = imageUrl;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
     public async Task<bool> UpdateUsername(Guid userId, string username)
     {
        try
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.UserName = username;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
     }

    public async Task<bool> DeleteUser(Guid userId)
    {
        var user = await _dbContext.Users
            .Include(u => u.Reviews)
            .Include(u => u.Requests)
            .Include(u => u.FavouriteTabletops)
            .Include(u => u.FavouriteVideogames)
            .Include(u => u.TabletopLibrary)
            .Include(u => u.VideogameLibrary)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        // Remove related entities first
        _dbContext.Reviews.RemoveRange(user.Reviews);
        _dbContext.Requests.RemoveRange(user.Requests);

        _dbContext.FavouriteTabletops.RemoveRange(user.FavouriteTabletops);
        _dbContext.FavouriteVideogames.RemoveRange(user.FavouriteVideogames);
        _dbContext.TabletopLibraries.RemoveRange(user.TabletopLibrary);
        _dbContext.VideogameLibraries.RemoveRange(user.VideogameLibrary);

        // Remove user
        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}