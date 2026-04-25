using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class ReviewRepository(PlayLibDContext context) : IReviewRepository {
    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Review>> GetByVideogameId(Guid videogameId) {
        return await _dbContext.Reviews.Where(r => r.VideogameId == videogameId).ToListAsync();
    }

    public async Task<bool> Create(Review review) {
        try {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }

    public async Task<bool> Delete(Guid reviewId) {
        var review = await _dbContext.Reviews.FindAsync(reviewId);
        if (review == null) return false;
        try {
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }
}
