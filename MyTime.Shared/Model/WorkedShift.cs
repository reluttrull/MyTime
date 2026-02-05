using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class WorkedShift : Entity
    {
        public int WorkedUserId { get; set; }
        public User? WorkedUser { get; set; }
        public int? ScheduledShiftId { get; set; }
        public ScheduledShift? ScheduledShift { get; set; }
        public int PunchInId { get; set; }
        public Punch? PunchIn { get; set; }
        public int PunchOutId { get; set; }
        public Punch? PunchOut { get; set; }
        public int? ApprovedByUserId { get; set; }
        public User? ApprovedByUser { get; set; }
        public DateTime? ApprovedOnDateUtc { get; set; }
    }
}
