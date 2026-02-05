using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public static class Enums
    {
        public enum PunchType
        {
            In = 0,
            Out = 1
        }
        public enum RequestType
        {
            Cancel = 0,
            Change = 1,
            Other = 99
        }
        public enum RequestStatus
        {
            Pending = 0,
            Approved = 1,
            Denied = 2,
            Other = 99
        }
    }
}
