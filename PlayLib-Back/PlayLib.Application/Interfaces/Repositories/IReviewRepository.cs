namespace PlayLib.Application.Interfaces.Repositories;

public interface IReviewRepository {
    Task<IEnumerable<Review>> GetByVideogameId(Guid videogameId);

    Task<Review?> GetById(Guid reviewId);

    Task<bool> Create(Review review);

    Task<bool> Delete(Guid reviewId);
}
