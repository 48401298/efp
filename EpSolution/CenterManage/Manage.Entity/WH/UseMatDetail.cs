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
    /// 领料数量明细
    /// </summary>
    [DBTable(TableName = "T_WH_UseMatDetail")]
    public class UseMatDetail
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///领料单主键
        ///</summary>
        [DBColumn(ColumnName = "USEID", DataType = DbType.String)]
        public string USEID { get; set; }

        ///<summary>
        ///库位
        ///</summary>
        [DBColumn(ColumnName = "SaveSite", DataType = DbType.String)]
        public string SaveSite { get; set; }

        ///<summary>
        ///货品唯一识别码
        ///</summary>
        [DBColumn(ColumnName = "MatBarCode", DataType = DbType.String)]
        public string MatBarCode { get; set; }

        ///<summary>
        ///物料主键
        ///</summary>
        [DBColumn(ColumnName = "MATRIALID", DataType = DbType.String)]
        public string MATRIALID { get; set; }

        ///<summary>
        ///数量
        ///</summary>
        [DBColumn(ColumnName = "AMOUNT", DataType = DbType.String)]
        public decimal AMOUNT { get; set; }

        ///<summary>
        ///计量单位
        ///</summary>
        [DBColumn(ColumnName = "Unit", DataType = DbType.String)]
        public string Unit { get; set; }

        public string MatName { get; set; }
    }
}
