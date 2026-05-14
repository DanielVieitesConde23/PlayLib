namespace PlayLib.Application.Interfaces;

public interface ILanguageService {
    Task<IEnumerable<object>> GetAllLanguages();
}
