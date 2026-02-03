using System;
using System.Collections.Generic;
using System.Text;
using static MyTime.Shared.Model.Enums;

namespace MyTime.Shared.Model
{
    public class Punch : Entity
    {
        public int PunchedUserId { get; set; }
        public DateTime PunchedTime { get; set; }
        public PunchType PunchType { get; set; }
    }
}
