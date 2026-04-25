using PlayLib.Data.DTOs;

namespace PlayLib.Application.Interfaces;

public interface IRequestService {
    Task<IEnumerable<RequestDTO>> GetRequestsForUser(Guid userId);

    Task<bool> CreateRequest(RequestDTO requestDto);

    Task<bool> ApproveRequest(Guid requestId);

    Task<bool> DenyRequest(Guid requestId);

    Task<IEnumerable<RequestDTO>> GetAllRequests();

    Task<bool> RemoveRequest(Guid requestId);
}
