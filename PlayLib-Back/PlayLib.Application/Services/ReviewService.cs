using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;

namespace PlayLib.Application.Services;

public class ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository) : IReviewService {
    private readonly IReviewRepository _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<IEnumerable<ReviewDTO>> GetReviewsForVideogame(Guid videogameId) {
        var reviews = await _reviewRepository.GetByVideogameId(videogameId);
        var result = new List<ReviewDTO>();
        foreach (var r in reviews) {
            var user = await _userRepository.GetById(r.UserId);
            result.Add(new ReviewDTO {
                Id = r.Id,
                Username = user?.UserName ?? "",
                ReviewDate = r.ReviewDate,
                Rating = r.Rating,
                Content = r.Content
            });
        }
        return result;
    }

    public async Task<bool> CreateReview(ReviewDTO reviewDto) {
        var review = new Review {
            Id = reviewDto.Id == Guid.Empty ? Guid.NewGuid() : reviewDto.Id,
            UserId = reviewDto.Username != null ? (await _userRepository.GetByUsername(reviewDto.Username))?.Id ?? Guid.Empty : Guid.Empty,
            VideogameId = null,
            ReviewDate = reviewDto.ReviewDate,
            Rating = reviewDto.Rating,
            Content = reviewDto.Content
        };

        return await _reviewRepository.Create(review);
    }

    public async Task<bool> DeleteReview(Guid reviewId) {
        return await _reviewRepository.Delete(reviewId);
    }
}

