using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Entity;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：要货原料信息
    ///　作    者：wanglu
    ///　编写日期：2018年01月10日
    /// </summary>
    [DBTable(TableName = "T_FP_SUPPLYMATERIALINFO")]
    public class SupplyMaterialInfo : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///要货信息主键
        ///</summary>
        [DBColumn(ColumnName = "SUPPLYID", DataType = DbType.String)]
        public string SUPPLYID { get; set; }

        ///<summary>
        ///物料主键
        ///</summary>
        [DBColumn(ColumnName = "MATRIALID", DataType = DbType.String)]
        public string MATRIALID { get; set; }

        ///<summary>
        ///数量
        ///</summary>
        [DBColumn(ColumnName = "AMOUNT", DataType = DbType.Int64)]
        public int AMOUNT { get; set; }

        ///<summary>
        ///单位
        ///</summary>
        [DBColumn(ColumnName = "Unit", DataType = DbType.String)]
        public string Unit { get; set; }
        
        ///<summary>
        ///删除
        ///</summary>
        public String DeleteAction { get; set; }

        ///<summary>
        ///物料名称
        ///</summary>
        public String MATRIALNAME { get; set; }

        ///<summary>
        ///单位名称
        ///</summary>
        public String UNITNAME { get; set; }
    }
}

