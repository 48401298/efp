using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.MES
{
    /// <summary>
    /// 
    /// </summary>
    public class OnProcessingInfo
    {
        public string PID { get; set; }

        public DateTime WorkingStartTime { get; set; }

        public string StartTime { get; set; }

        public string CurrentTime { get; set; }

        public string SpendTime { get; set; }

        public string MatName { get; set; }

        public string EName { get; set; }

        public string ProcessName { get; set; }

        public string ProductName { get; set; }
    }
}
