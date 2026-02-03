using MyTime.Shared.Model;
using MyTime.Shifts.Contracts.Requests;
using MyTime.Shifts.Contracts.Responses;

namespace MyTime.ApiService
{
    public static class ContractMapping
    {
        public static Punch MapFromRequest(this CreatePunchRequest request)
        {
            return new Punch
            {
                PunchedUserId = request.UserId,
                PunchedTime = DateTime.UtcNow
            };
        }

        public static PunchResponse MapToResponse(this Punch punch)
        {
            return new PunchResponse(punch.Id, punch.PunchedUserId, punch.PunchedTime, punch.PunchType, null, null);
        }
    }
}
