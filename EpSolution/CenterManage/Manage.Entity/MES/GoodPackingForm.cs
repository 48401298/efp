using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// <summary>
    /// 产品包装组成
    /// </summary>
    [DBTable(TableName = "T_FP_GoodPackingForm")]
    public class GoodPackingForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        /// <summary>
        /// 主条码
        /// </summary>
        [DBColumn(ColumnName = "GoodBarCode", DataType = DbType.String)]
        public string GoodBarCode { get; set; }

        /// <summary>
        /// 小包装条码
        /// </summary>
        [DBColumn(ColumnName = "DetailBarCode", DataType = DbType.String)]
        public string DetailBarCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [DBColumn(ColumnName = "BatchNumber", DataType = DbType.String)]
        public string BatchNumber { get; set; }
    }
}
