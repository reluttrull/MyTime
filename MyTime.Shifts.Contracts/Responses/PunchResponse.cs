namespace MyTime.Shifts.Contracts.Responses
{
    public record PunchResponse(int PunchId, int UserId, DateTime RecordedTime, bool? IsApproved, int? ApprovedByUser);
}
