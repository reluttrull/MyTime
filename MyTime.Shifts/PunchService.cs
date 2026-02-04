using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shared.Model;
using MyTime.Shifts.Contracts.Requests;
using MyTime.Shifts.Contracts.Responses;

namespace MyTime.Shifts
{
    public class PunchService(MyTimeDbContext context) : IPunchService
    {
        private readonly MyTimeDbContext _context = context;

        public async Task<PunchResponse> CreatePunchAsync(CreatePunchRequest request, CancellationToken token = default)
        {
            Punch punch = request.MapFromRequest();
            PunchResponse? lastPunch = await GetLastPunchForUserAsync(punch.PunchedUserId, token);
            punch.PunchType = lastPunch is null 
                ? Enums.PunchType.In 
                : (lastPunch.PunchType == Enums.PunchType.In 
                    ? Enums.PunchType.Out : Enums.PunchType.In);
            // todo: if punch out, also create worked shift
            // todo: if punch out, also create event "CompletedShift"
            _context.Punches.Add(punch);
            await _context.SaveChangesAsync(cancellationToken: token);
            PunchResponse? createdPunch = await GetPunchByIdAsync(punch.Id, token);
            return createdPunch ?? punch.MapToResponse();
        }

        public async Task<bool> DeletePunchAsync(int id, CancellationToken token = default)
        {
            Punch? punchToDelete = await _context.Punches.FindAsync([id], cancellationToken: token);
            if (punchToDelete is null) return false;
            _context.Punches.Remove(punchToDelete);
            await _context.SaveChangesAsync(cancellationToken: token);
            // todo: logging
            return true;
        }

        public async Task<List<PunchResponse>> GetAllPunchesAsync(int? userId, CancellationToken token = default)
        {
            return await _context.Punches
                .WhereIf(userId is not null, p => p.PunchedUserId == userId)
                .OrderByDescending(p => p.PunchedTime)
                .Select(p => p.MapToResponse())
                .ToListAsync<PunchResponse>(token);
        }

        public async Task<PunchResponse?> GetLastPunchForUserAsync(int userId, CancellationToken token = default)
        {
            var lastPunch = await _context.Punches
                .OrderByDescending(p => p.PunchedTime)
                .FirstOrDefaultAsync(p => p.PunchedUserId == userId, cancellationToken: token);
            return lastPunch?.MapToResponse();
        }

        public async Task<PunchResponse?> GetPunchByIdAsync(int id, CancellationToken token = default)
        {
            var punch = await _context.Punches
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken: token);
            return punch?.MapToResponse();
        }
    }

    public interface IPunchService
    {
        Task<PunchResponse?> GetLastPunchForUserAsync(int userId, CancellationToken token = default);
        Task<PunchResponse?> GetPunchByIdAsync(int id, CancellationToken token = default);
        Task<List<PunchResponse>> GetAllPunchesAsync(int? userId, CancellationToken token = default);
        Task<PunchResponse> CreatePunchAsync(CreatePunchRequest request, CancellationToken token = default);
        Task<bool> DeletePunchAsync(int id, CancellationToken token = default);
    }
}
