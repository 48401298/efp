using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：班组人员关系表
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_EQUIPMENTREF")]
    public class EquipmentRef
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///工序主键
        ///</summary>
        [DBColumn(ColumnName = "PRID", DataType = DbType.String)]
        public string PRID { get; set; }

        ///<summary>
        ///设备主键
        ///</summary>
        [DBColumn(ColumnName = "EQID", DataType = DbType.String)]
        public string EQID { get; set; }

        public string EQNAME { get; set; }

        public string DeleteAction { get; set; }
    }
}
