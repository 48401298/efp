using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manage.Entity.BI
{
    /// <summary>
    /// 柱状图数据
    /// </summary>
    public class BarChartData
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<decimal> data { get; set; }
    }
}