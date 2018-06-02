using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产品BOM明细
    ///　作    者：wanglu
    ///　编写日期：2018年01月01日
    /// </summary>
    [DBTable(TableName = "T_FP_BOMDETAIL")]
    public class BOMDetail : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///BOM主键
        ///</summary>
        [DBColumn(ColumnName = "BOMID", DataType = DbType.String)]
        public string BOMID { get; set; }

        ///<summary>
        ///物料主键
        ///</summary>
        [DBColumn(ColumnName = "MATRIALID", DataType = DbType.String)]
        public String MATRIALID { get; set; }

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
        ///物料名称
        ///</summary>
        public String MATRIALNAME { get; set; }

        ///<summary>
        ///单位名称
        ///</summary>
        public String UNITNAME { get; set; }

        ///<summary>
        ///删除
        ///</summary>
        public String DeleteAction { get; set; }

    }
}
