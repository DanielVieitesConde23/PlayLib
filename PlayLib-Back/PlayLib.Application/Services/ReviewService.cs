using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;

namespace PlayLib.Application.Services;

public class ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IVideogameService videogameService, ITabletopService tabletopService) : IReviewService {
    private readonly IReviewRepository _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IVideogameService _videogameService = videogameService ?? throw new ArgumentNullException(nameof(videogameService));
    private readonly ITabletopService _tabletopService = tabletopService ?? throw new ArgumentNullException(nameof(tabletopService));


    public async Task<IEnumerable<ReviewDTO>> GetReviewsForVideogame(Guid videogameId) {
        var reviews = await _reviewRepository.GetByVideogameId(videogameId);
        var result = new List<ReviewDTO>();
        foreach (var r in reviews) {
            var user = await _userRepository.GetById(r.UserId);
            result.Add(new ReviewDTO {
                Id = r.Id,
                UserId = r.UserId,
                Username = user?.UserName ?? "",
                UserImage = user?.Profile_Img ?? "",
                ReviewDate = r.ReviewDate,
                Rating = r.Rating,
                Content = r.Content
            });
        }
        return result;
    }

    public async Task<bool> CreateReview(ReviewDTO reviewDto) {
        var isVideogame = await _videogameService.VideogameExists(reviewDto.GameId);
        var isTabletop = await _tabletopService.TabletopExists(reviewDto.GameId);

        // invalid id
        if (!isVideogame && !isTabletop)
            return false;

        // optional sanity check
        if (isVideogame && isTabletop)
            throw new Exception("Game exists in both tables.");

        var review = new Review
        {
            Id = reviewDto.Id == Guid.Empty ? Guid.NewGuid() : reviewDto.Id,

            UserId = reviewDto.UserId,

            VideogameId = isVideogame ? reviewDto.GameId : null,

            TabletopGameId = isTabletop ? reviewDto.GameId : null,

            ReviewDate = reviewDto.ReviewDate,
            Rating = reviewDto.Rating,
            Content = reviewDto.Content
        };

        return await _reviewRepository.Create(review);
    }

    public async Task<bool> DeleteReview(Guid reviewId, Guid userId) {
        var review = await _reviewRepository.GetById(reviewId);
        if (review == null) return false;

        var user = await _userRepository.GetById(userId);
        if (user == null) return false;

        if (review.UserId != userId && user.Role != "Administrator") {
            return false;
        }

        return await _reviewRepository.Delete(reviewId);
    }
}

