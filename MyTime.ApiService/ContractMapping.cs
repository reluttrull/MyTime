using MyTime.Shared.Model;
using MyTime.Shifts.Contracts.Responses;

namespace MyTime.ApiService
{
    public static class ContractMapping
    {
        public static PunchResponse MapToResponse(this Punch punch)
        {
            return new PunchResponse(punch.Id, punch.PunchedUserId, punch.PunchedTime, null, null);
        }
    }
}
