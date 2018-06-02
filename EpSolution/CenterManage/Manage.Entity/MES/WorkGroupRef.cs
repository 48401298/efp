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
    [DBTable(TableName = "T_FP_WORKGROUPREF")]
    public class WorkGroupRef
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///班组主键
        ///</summary>
        [DBColumn(ColumnName = "WOID", DataType = DbType.String)]
        public string WOID { get; set; }

        ///<summary>
        ///人员主键
        ///</summary>
        [DBColumn(ColumnName = "PEID", DataType = DbType.String)]
        public string PEID { get; set; }
    }
}
