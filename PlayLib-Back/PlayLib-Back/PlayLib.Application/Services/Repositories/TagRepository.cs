using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class TagRepository(PlayLibDContext context) : ITagRepository {
    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Tag>> GetAll() {
        return await _dbContext.Tags.ToListAsync();
    }
}
