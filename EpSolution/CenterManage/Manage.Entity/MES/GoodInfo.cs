using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// <summary>
    /// 产成品记录
    /// </summary>
    [DBTable(TableName = "T_FP_GoodInfo")]
    public class GoodInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        [DBColumn(ColumnName = "FACTORYPID", DataType = DbType.String)]
        public string FACTORYPID { get; set; }

        [DBColumn(ColumnName = "PRID", DataType = DbType.String)]
        public string PRID { get; set; }

        [DBColumn(ColumnName = "WOID", DataType = DbType.String)]
        public string WOID { get; set; }

        [DBColumn(ColumnName = "Operator", DataType = DbType.String)]
        public string Operator { get; set; }

        [DBColumn(ColumnName = "PLANDATE", DataType = DbType.DateTime)]
        public DateTime PLANDATE { get; set; }

        [DBColumn(ColumnName = "ProductionID", DataType = DbType.String)]
        public string ProductionID { get; set; }

        [DBColumn(ColumnName = "GoodBarCode", DataType = DbType.String)]
        public string GoodBarCode { get; set; }

        [DBColumn(ColumnName = "BatchNumber", DataType = DbType.String)]
        public string BatchNumber { get; set; }

        
        ///<summary>
        ///创建人
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///更新人
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///更新时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string SPECIFICATION { get; set; }
        public Int64 OfflineNum { get; set; }
        public Int64 FactAmount { get; set; }
        public string PLANID { get; set; }
        

    }
}
