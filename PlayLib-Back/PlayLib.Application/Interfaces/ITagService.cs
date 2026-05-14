namespace PlayLib.Application.Interfaces;

public interface ITagService {
    Task<IEnumerable<object>> GetAllTags();
}
