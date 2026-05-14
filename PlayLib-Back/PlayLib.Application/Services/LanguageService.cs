using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;

namespace PlayLib.Application.Services;

public class LanguageService(ILanguageRepository languageRepository) : ILanguageService {
    private readonly ILanguageRepository _languageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));

    public async Task<IEnumerable<object>> GetAllLanguages() {
        var languages = await _languageRepository.GetAll();
        return languages.Select(l => new { l.Id, l.Name });
    }
}
