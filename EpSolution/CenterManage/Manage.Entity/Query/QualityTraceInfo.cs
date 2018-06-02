using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.Query
{
    /// <summary>
    /// 质量追溯产品信息
    /// </summary>
    public class QualityTraceInfo
    {
        public string PID { get; set; }

        public string GoodBarCode { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string BatchNumber { get; set; }

        public string FactoryName { get; set; }

        public string LineName { get; set; }

        public string PLANDATE { get; set; }
    }
}
