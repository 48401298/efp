using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.WH
{
    public class QualityCheckCondition
    {
        public string BillNO { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string CheckResult { get; set; }

        public string FACTORYPID { get; set; }

        public string PRODUCEID { get; set; }
    }
}
