using PlayLib.Data.Entities;

namespace PlayLib.Application.Interfaces.Repositories;

public interface IRequestRepository {
    Task<IEnumerable<Request>> GetByUserId(Guid userId);

    Task<bool> Create(Request request);

    Task<bool> SetApproved(Guid requestId);

    Task<bool> SetDenied(Guid requestId);

    Task<IEnumerable<Request>> GetAll();

    Task<bool> Delete(Guid requestId);
}
