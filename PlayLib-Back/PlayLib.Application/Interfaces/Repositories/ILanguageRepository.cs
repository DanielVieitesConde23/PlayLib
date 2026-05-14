using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface ILanguageRepository {
    Task<IEnumerable<Language>> GetAll();
}
