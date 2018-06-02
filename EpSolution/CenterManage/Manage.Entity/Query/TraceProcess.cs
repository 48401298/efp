using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.Query
{
    public class TraceProcess
    {
        public string PID { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string WorkingStartTime { get; set; }
        public string WorkingEndTime { get; set; }
        public string EquCode { get; set; }
        public string EquName { get; set; }
    }
}
