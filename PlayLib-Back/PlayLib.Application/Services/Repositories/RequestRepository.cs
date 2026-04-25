using Microsoft.EntityFrameworkCore;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Data.Entities;

namespace PlayLib.Application.Services.Repositories;

public class RequestRepository(PlayLibDContext context) : IRequestRepository {
    private readonly PlayLibDContext _dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Request>> GetByUserId(Guid userId) {
        return await _dbContext.Requests.Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<bool> Create(Request request) {
        try {
            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return true;
        } catch {
            return false;
        }
    }

    public async Task<bool> SetApproved(Guid requestId) {
        var req = await _dbContext.Requests.FindAsync(requestId);
        if (req == null) return false;
        req.Approved = true;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetDenied(Guid requestId)
    {
        var req = await _dbContext.Requests.FindAsync(requestId);
        if (req == null)
            return false;
        req.Approved = false;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Request>> GetAll()
    {
        return await _dbContext.Requests.ToListAsync();
    }

    public async Task<bool> Delete(Guid requestId)
    {
        var req = await _dbContext.Requests.FindAsync(requestId);
        if (req == null)
            return false;
        _dbContext.Requests.Remove(req);
        await _dbContext.SaveChangesAsync();
        return true;
    }   
}
