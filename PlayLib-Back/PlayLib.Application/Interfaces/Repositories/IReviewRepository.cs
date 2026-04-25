namespace PlayLib.Application.Interfaces.Repositories;

public interface IReviewRepository {
    Task<IEnumerable<Review>> GetByVideogameId(Guid videogameId);

    Task<bool> Create(Review review);

    Task<bool> Delete(Guid reviewId);
}
