using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface ITabletopRepository {

    Task<bool> TabletopExists(Guid tabletopId);

    Task<TabletopGame> GetTabletop(Guid tabletopId);

    Task<IEnumerable<TabletopGame>> GetTabletopsByTag(string tag, Guid userId);

    Task<IEnumerable<TabletopGame>> GetMostPopularTabletops(Guid userId);

    Task<string> GetMostPupularTagForUser(Guid userId);
}
