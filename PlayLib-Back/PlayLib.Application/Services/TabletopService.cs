using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services;

public class TabletopService(ITabletopRepository tabletopRepository) : ITabletopService {

    private readonly ITabletopRepository _tabletopRepository = tabletopRepository ?? throw new ArgumentNullException(nameof(tabletopRepository));

    public async Task<bool> TabletopExists(Guid tabletopId)
    {
        return await _tabletopRepository.TabletopExists(tabletopId);
    }

    public async Task<TabletopWithReviews> GetTabletopWithReviews(Guid tabletopId, Guid userId)
    {
        var tabletop = await _tabletopRepository.GetTabletop(tabletopId);
        if (tabletop == null) return null;

        return new TabletopWithReviews
        {
            Id = tabletop.Id,
            Name = tabletop.Name,
            Description = tabletop.Description,
            Developer = tabletop.Creator,
            ImageRoute = tabletop.ImageRoute,
            ReleaseDate = tabletop.ReleaseDate,
            AverageDuration = tabletop.AverageDuration,
            MaxPlayerNumber = tabletop.MaxPlayerNumber,
            MinPlayerNumber = tabletop.MinPlayerNumber,
            TimesPlayed = tabletop.Libraries.FirstOrDefault(t => t.UserId == userId)?.TimesPlayed ?? 0,
            IsFavourite = tabletop.Favourites.Any(f => f.UserId == userId),
            IsInLibrary = tabletop.Libraries.Any(l => l.UserId == userId),
            Reviews = tabletop.Reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                Username = r.User?.UserName ?? "",
                ReviewDate = r.ReviewDate,
                Rating = r.Rating,
                Content = r.Content
            }).ToList(),
            Tags = tabletop.Tags.Select(t => new TagDto
            {
                Id = t.Tag.Id,
                Name = t.Tag.Name,
                Hex = t.Tag.Hex
            }).ToList(),
            Languages = tabletop.Languages.Select(l => new LanguageDTO
            {
                Id = l.Language.Id,
                Name = l.Language.Name
            }).ToList()
        };
    }

    public async Task<IEnumerable<TabletopCarrousel>> GetTabletopsByTag(string tag, Guid userId)
    {
        var tabletops = await _tabletopRepository.GetTabletopsByTag(tag, userId);
        if (tabletops == null || !tabletops.Any()) return null;

        return tabletops.Select(v => new TabletopCarrousel
        {
            Id = v.Id,
            Name = v.Name,
            Image = v.ImageRoute
        }).ToList();
    }

    public async Task<IEnumerable<TabletopCarrousel>> GetMostPopularTabletops(Guid userId)
    {
        var tabletops = await _tabletopRepository.GetMostPopularTabletops(userId);
        if (tabletops == null || !tabletops.Any())
            return null;

        return tabletops.Select(v => new TabletopCarrousel
        {
            Id = v.Id,
            Name = v.Name,
            Image = v.ImageRoute
        }).ToList();
    }

    public async Task<string> GetMostPupularTagForUser(Guid userId)
    {
        return await _tabletopRepository.GetMostPupularTagForUser(userId);
    }

    public async Task<bool> UpdateTabletopPlayedGames(Guid tabletopId, Guid userId, int playedGames)
    {
        return await _tabletopRepository.UpdateTabletopPlayedGames(tabletopId, userId, playedGames);
    }

    public async Task<bool> CreateTabletopGame(CreateTabletopGameDTO dto)
    {
        try
        {
            var tabletopGame = new TabletopGame
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Creator = dto.Creator,
                ImageRoute = dto.Image_Route,
                ReleaseDate = dto.Release_Date,

                MinPlayerNumber = dto.MinPlayerNumber,
                MaxPlayerNumber = dto.MaxPlayerNumber,
                AverageDuration = dto.AverageDuration,

                Tags = dto.Tags.Select(tagId => new TagTabletop
                {
                    TagId = tagId
                }).ToList(),

                Languages = dto.Languages.Select(languageId => new LanguageTabletop
                {
                    LanguageId = languageId
                }).ToList()
            };

            await _tabletopRepository.CreateTabletopGame(tabletopGame);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
