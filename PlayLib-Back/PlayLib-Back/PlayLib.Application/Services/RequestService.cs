using PlayLib.Application.Interfaces;
using PlayLib.Data.DTOs;
using PlayLib.Application.Interfaces.Repositories;

namespace PlayLib.Application.Services;

public class RequestService(IRequestRepository requestRepository) : IRequestService {
    private readonly IRequestRepository _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));

    public async Task<IEnumerable<RequestDTO>> GetRequestsForUser(Guid userId) {
        var requests = await _requestRepository.GetByUserId(userId);
        return requests.Select(r => new RequestDTO {
            Id = r.Id,
            UserId = r.UserId,
            GameName = r.GameName,
            IsTabletop = r.IsTabletop,
            Description = r.Description,
            Approved = r.Approved
        });
    }

    public async Task<bool> CreateRequest(RequestDTO requestDto) {
        var req = new Request {
            Id = Guid.NewGuid(),
            UserId = requestDto.UserId,
            GameName = requestDto.GameName,
            IsTabletop = requestDto.IsTabletop,
            Description = requestDto.Description,
            Approved = null
        };

        return await _requestRepository.Create(req);
    }

    public async Task<bool> ApproveRequest(Guid requestId) {
        return await _requestRepository.SetApproved(requestId);
    }

    public async Task<bool> DenyRequest(Guid requestId)
    {
        return await _requestRepository.SetDenied(requestId);
    }

    public async Task<IEnumerable<RequestDTO>> GetAllRequests()
    {
        var requests = await _requestRepository.GetAll();
        return requests.Select(r => new RequestDTO {
            Id = r.Id,
            UserId = r.UserId,
            GameName = r.GameName,
            IsTabletop = r.IsTabletop,
            Description = r.Description,
            Approved = r.Approved
        });
    }

    public async Task<bool> RemoveRequest(Guid requestId)
    {
        return await _requestRepository.Delete(requestId);
    }
}
