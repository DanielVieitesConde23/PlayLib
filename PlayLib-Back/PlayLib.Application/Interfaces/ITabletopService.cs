using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface ITabletopService {
    Task<bool> TabletopExists(Guid tabletopId);
    Task<TabletopWithReviews> GetTabletopWithReviews(Guid tabletopId, Guid userId);
    Task<IEnumerable<TabletopCarrousel>> GetTabletopsByTag(string tag, Guid userId);
    Task<IEnumerable<TabletopCarrousel>> GetMostPopularTabletops(Guid userId);
    Task<string> GetMostPupularTagForUser(Guid userId);
}
