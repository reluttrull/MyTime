using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class DirectReport : Entity
    {
        public int ReportingUserId { get; set; }
        public User? ReportingUser { get; set; }
        public int ReportsToUserId { get; set; }
        public User? ReportsToUser { get; set; }
        public DateTime AddedDateUtc { get; set; }
        public int AddedByUserId { get; set; }
        public User? AddedByUser { get; set; }
    }
}
