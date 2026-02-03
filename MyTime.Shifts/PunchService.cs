using Microsoft.EntityFrameworkCore;
using MyTime.Shared.Data;
using MyTime.Shared.Extensions;
using MyTime.Shared.Model;

namespace MyTime.Shifts
{
    public class PunchService(MyTimeDbContext context) : IPunchService
    {
        private readonly MyTimeDbContext _context = context;
        public async Task<List<Punch>> GetAllPunchesAsync(int? userId, CancellationToken token = default)
        {
            return await _context.Punches
                .WhereIf(userId is not null, p => p.PunchedUserId == userId)
                .OrderByDescending(p => p.PunchedTime)
                .ToListAsync<Punch>(token);
        }
    }

    public interface IPunchService
    {
        Task<List<Punch>> GetAllPunchesAsync(int? userId, CancellationToken token = default);
    }
}
