using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 货品质量检查单
    /// </summary>
    [DBTable(TableName = "T_WH_QualityCheck")]
    public class WHQualityCheck
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 质检单号
        /// </summary>
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        /// <summary>
        /// 检查日期
        /// </summary>
        [DBColumn(ColumnName = "CheckDate", DataType = DbType.DateTime)]
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 入库单主键
        /// </summary>
        [DBColumn(ColumnName = "BillID", DataType = DbType.String)]
        public string BillID { get; set; }

        ///<summary>
        ///质检人员
        ///</summary>
        [DBColumn(ColumnName = "CheckPerson", DataType = DbType.String)]
        public string CheckPerson { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        [DBColumn(ColumnName = "InStockBillNo", DataType = DbType.String)]
        public string InStockBillNo { get; set; }

        ///<summary>
        ///质检描述
        ///</summary>
        [DBColumn(ColumnName = "CheckResult", DataType = DbType.String)]
        public String CheckResult { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///创建者
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///修改者
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///修改时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public List<WHQualityCheckResult> QualityCheckResults { get; set; }
        
    }
}
