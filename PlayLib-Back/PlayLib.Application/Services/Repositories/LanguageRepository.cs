using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class LanguageRepository(PlayLibDContext context) : ILanguageRepository {
    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Language>> GetAll() {
        return await _dbContext.Languages.ToListAsync();
    }
}
