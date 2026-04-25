using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface ITagRepository {
    Task<IEnumerable<Tag>> GetAll();
}
