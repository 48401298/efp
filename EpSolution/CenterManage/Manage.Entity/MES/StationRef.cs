using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：工艺工位关系表
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_STATIONREF")]
    public class StationRef
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
        ///工位主键
        ///</summary>
        [DBColumn(ColumnName = "STID", DataType = DbType.String)]
        public string STID { get; set; }

        public string STNAME { get; set; }

        public string DeleteAction { get; set; }
    }
}
