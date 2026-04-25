using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface IReviewService {
    Task<IEnumerable<ReviewDTO>> GetReviewsForVideogame(Guid videogameId);

    Task<bool> CreateReview(ReviewDTO reviewDto);

    Task<bool> DeleteReview(Guid reviewId);
}
