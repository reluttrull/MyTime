using MyTime.Shared.Model;

namespace MyTime.Shifts.Contracts.Responses
{
    public record PunchResponse(int Id, int UserId, DateTime RecordedTime, Enums.PunchType PunchType, bool? IsApproved, int? ApprovedByUser);
}
