using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;

namespace PlayLib.Application.Services;

public class TagService(ITagRepository tagRepository) : ITagService {
    private readonly ITagRepository _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<IEnumerable<string>> GetAllTags() {
        var tags = await _tagRepository.GetAll();
        return tags.Select(t => t.Name);
    }
}
