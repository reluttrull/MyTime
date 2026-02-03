using MyTime.Shared.Model;

namespace MyTime.Shifts.Contracts.Responses
{
    public record PunchResponse(int PunchId, int UserId, DateTime RecordedTime, Enums.PunchType PunchType, bool? IsApproved, int? ApprovedByUser);
}
