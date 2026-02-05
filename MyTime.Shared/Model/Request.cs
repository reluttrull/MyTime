using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class Request : Entity
    {
        public int ScheduledShiftId { get; set; }
        public ScheduledShift? ScheduledShift { get; set; }
        public Enums.RequestType RequestType { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RequestedDateUtc { get; set; }
        public int RequestedByUserId { get; set; }
        public User? RequestedByUser { get; set; }
        public Enums.RequestStatus RequestStatus { get; set; }
    }
}
