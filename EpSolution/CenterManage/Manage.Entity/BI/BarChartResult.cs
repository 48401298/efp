using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.BI
{
    /// <summary>
    /// 柱状图分析结果
    /// </summary>
    public class BarChartResult
    {
        public List<string> XAxisData { get; set; }

        public List<BarChartData> Series { get; set; }
    }
}
