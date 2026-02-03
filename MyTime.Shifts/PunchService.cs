using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shared.Model;

namespace MyTime.Shifts
{
    public class PunchService(MyTimeDbContext context) : IPunchService
    {
        private readonly MyTimeDbContext _context = context;

        public async Task<Punch> CreatePunchAsync(Punch punch, CancellationToken token = default)
        {
            Punch? lastPunch = await GetLastPunchForUserAsync(punch.PunchedUserId, token);
            punch.PunchType = lastPunch is null 
                ? Enums.PunchType.In 
                : (lastPunch.PunchType == Enums.PunchType.In 
                    ? Enums.PunchType.Out : Enums.PunchType.In);
            // todo: if punch out, also create worked shift
            // todo: if punch out, also create event "CompletedShift"
            _context.Punches.Add(punch);
            await _context.SaveChangesAsync(cancellationToken: token);
            Punch? createdPunch = await GetPunchByIdAsync(punch.Id, token);
            return createdPunch ?? punch;
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

        public async Task<List<Punch>> GetAllPunchesAsync(int? userId, CancellationToken token = default)
        {
            return await _context.Punches
                .WhereIf(userId is not null, p => p.PunchedUserId == userId)
                .OrderByDescending(p => p.PunchedTime)
                .ToListAsync<Punch>(token);
        }

        public async Task<Punch?> GetLastPunchForUserAsync(int userId, CancellationToken token = default)
        {
            return await _context.Punches
                .OrderByDescending(p => p.PunchedTime)
                .FirstOrDefaultAsync(p => p.PunchedUserId == userId, cancellationToken: token);
        }

        public async Task<Punch?> GetPunchByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Punches
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken: token);
        }
    }

    public interface IPunchService
    {
        Task<Punch?> GetLastPunchForUserAsync(int userId, CancellationToken token = default);
        Task<Punch?> GetPunchByIdAsync(int id, CancellationToken token = default);
        Task<List<Punch>> GetAllPunchesAsync(int? userId, CancellationToken token = default);
        Task<Punch> CreatePunchAsync(Punch punch, CancellationToken token = default);
        Task<bool> DeletePunchAsync(int id, CancellationToken token = default);
    }
}
