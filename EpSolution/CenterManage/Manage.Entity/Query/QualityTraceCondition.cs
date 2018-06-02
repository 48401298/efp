using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.Query
{
    /// <summary>
    /// 追溯查询条件
    /// </summary>
    public class QualityTraceCondition
    {
        /// <summary>
        /// 生产开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 生产截至日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProductLine { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// 产品批次
        /// </summary>
        public string ProductBatchNumber { get; set; }

        /// <summary>
        /// 原料识别码
        /// </summary>
        public string MatIDCode { get; set; }

        /// <summary>
        /// 产品识别码
        /// </summary>
        public string ProductIDCode { get; set; }
    }
}
