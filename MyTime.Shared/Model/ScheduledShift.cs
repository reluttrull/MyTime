using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class ScheduledShift : Entity
    {
        public int ScheduledUserId { get; set; }
        public User? ScheduledUser { get; set; }
        public int AddedByUserId { get; set; }
        public User? AddedByUser { get; set; }
        public DateTime ShiftStartTimeUtc { get; set; }
        public DateTime ShiftEndTimeUtc { get; set; }
    }
}
