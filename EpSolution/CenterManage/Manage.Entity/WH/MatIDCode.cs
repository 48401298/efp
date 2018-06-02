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
    /// 货品唯一识别码
    /// </summary>
    [DBTable(TableName = "T_WH_MatIDCode")]
    public class MatIDCode
    {
        ///<summary>
        ///唯一识别码
        ///</summary>
        [DBColumn(ColumnName = "IDCode", DataType = DbType.String, IsKey = true)]
        public string IDCode { get; set; }

        ///<summary>
        ///货品主键
        ///</summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        ///<summary>
        ///货品规格
        ///</summary>
        [DBColumn(ColumnName = "MatSpec", DataType = DbType.String)]
        public string MatSpec { get; set; }

        ///<summary>
        ///生成日期
        ///</summary>
        [DBColumn(ColumnName = "BuildDate", DataType = DbType.String)]
        public string BuildDate { get; set; }

        ///<summary>
        ///序号
        ///</summary>
        [DBColumn(ColumnName = "Seq", DataType = DbType.Int32)]
        public int Seq { get; set; }

        ///<summary>
        ///状态
        ///</summary>
        [DBColumn(ColumnName = "Status", DataType = DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        [DBColumn(ColumnName = "ProduceDate", DataType = DbType.String)]
        public string ProduceDate { get; set; }
        
        ///<summary>
        ///打印次数
        ///</summary>
        [DBColumn(ColumnName = "PrintCount", DataType = DbType.Int32)]
        public int PrintCount { get; set; }
    }
}
