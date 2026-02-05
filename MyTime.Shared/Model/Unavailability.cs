using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class Unavailability : Entity
    {
        public int UnavailableUserId { get; set; }
        public User? UnavailableUser { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public DateTime AddedDateUtc { get; set; }
        public int AddedUserId { get; set; }
        public User? AddedUser { get; set; }
    }
}
