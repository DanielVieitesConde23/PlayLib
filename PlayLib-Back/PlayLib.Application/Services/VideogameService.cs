using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.DTOs;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services;

public class VideogameService(IVideogameRepository videogameRepository) : IVideogameService {

    private readonly IVideogameRepository _videogameRepository = videogameRepository ?? throw new ArgumentNullException(nameof(videogameRepository));

    public async Task<bool> VideogameExists(Guid videogameId)
    {
        return await _videogameRepository.VideogameExists(videogameId);
    }

    public async Task<VideogameWithReviews> GetVideogameWithReviews(Guid videogameId, Guid userId)
    {
        var videogame = await _videogameRepository.GetVideogame(videogameId);
        if (videogame == null) return null;

        return new VideogameWithReviews
        {
            Id = videogameId,
            Name = videogame.Name,
            Description = videogame.Description,
            Developer = videogame.Developer,
            ImageRoute = videogame.ImageRoute,
            ReleaseDate = videogame.ReleaseDate,
            State = videogame.Libraries.FirstOrDefault(x => x.UserId == userId)?.State,
            Format = videogame.Libraries.FirstOrDefault(x => x.UserId == userId)?.Format,
            IsFavourite = videogame.Favourites.Any(f => f.UserId == userId),
            IsInLibrary = videogame.Libraries.Any(l => l.UserId == userId),
            Reviews = videogame.Reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                Username = r.User?.UserName ?? "",
                ReviewDate = r.ReviewDate,
                Rating = r.Rating,
                Content = r.Content
            }).ToList(),
            Tags = videogame.Tags.Select(t => new TagDto
            {
                Id = t.Tag.Id,
                Name = t.Tag.Name,
                Hex = t.Tag.Hex
            }).ToList(),
            Languages = videogame.Languages.Select(l => new LanguageDTO
            {
                Id = l.Language.Id,
                Name = l.Language.Name
            }).ToList()
        };
    }

    public async Task<IEnumerable<VideogameCarrousel>> GetVideogamesByTag(string tag, Guid userId)
    {
        var videogames = await _videogameRepository.GetVideogamesByTag(tag, userId);
        if (videogames == null || !videogames.Any()) return null;

        return videogames.Select(v => new VideogameCarrousel
        {
            Id = v.Id,
            Name = v.Name,
            Image = v.ImageRoute
        }).ToList();
    }

    public async Task<IEnumerable<VideogameCarrousel>> GetMostPopularGames(Guid userId)
    {
        var videogames = await _videogameRepository.GetMostPopularGames(userId);
        if (videogames == null || !videogames.Any())
            return null;

        return videogames.Select(v => new VideogameCarrousel
        {
            Id = v.Id,
            Name = v.Name,
            Image = v.ImageRoute
        }).ToList();
    }

    public async Task<string> GetMostPupularTagForUser(Guid userId)
    {
        return await _videogameRepository.GetMostPupularTagForUser(userId);
    }

    public async Task<List<GameSearchResult>> SearchGamesByName(string name)
    {
        return await _videogameRepository.SearchGamesByName(name);
    }

    public async Task<bool> UpdateVideogameState(Guid videogameId, Guid userId, string newState)
    {
        try
        {
            await _videogameRepository.UpdateLibraryState(videogameId, userId, newState);
            return true;
        }
        catch (Exception) 
        {
            return false;
        }
        
    }

    public async Task<bool> UpdateVideogameFormat(Guid videogameId, Guid userId, string newFormat)
    {
        try
        {
            await _videogameRepository.UpdateLibraryFormat(videogameId, userId, newFormat);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> CreateVideogame(CreateVideogameDTO videogameDTO)
    {
        try
        {

            var videogame = new Videogame
            {
                Id = Guid.NewGuid(),
                Name = videogameDTO.Name,
                Description = videogameDTO.Description,
                Developer = videogameDTO.Developer,
                ImageRoute = videogameDTO.Image_Route,
                ReleaseDate = videogameDTO.Release_Date,

                Tags = videogameDTO.Tags.Select(tagId => new TagVideogame
                {
                    TagId = tagId
                }).ToList(),

                Languages = videogameDTO.Languages.Select(languageId => new LanguageVideogame
                {
                    LanguageId = languageId
                }).ToList()
            };

            await _videogameRepository.CreateVideogame(videogame);

            return true;
        }
        catch
        {
            return false;
        }
    }
}