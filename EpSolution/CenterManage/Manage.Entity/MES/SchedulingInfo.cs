using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：排班信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_SCHEDULING")]
    public class SchedulingInfo
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///所属工厂主键
        ///</summary>
        [DBColumn(ColumnName = "FAID", DataType = DbType.String)]
        public string FAID { get; set; }

        ///<summary>
        ///生产线主键
        ///</summary>
        [DBColumn(ColumnName = "PRID", DataType = DbType.String)]
        public String PRID { get; set; }

        ///<summary>
        ///班组主键
        ///</summary>
        [DBColumn(ColumnName = "WOID", DataType = DbType.String)]
        public string WOID { get; set; }

        ///<summary>
        ///日期
        ///</summary>
        [DBColumn(ColumnName = "WORKDATE", DataType = DbType.DateTime)]
        public DateTime WORKDATE { get; set; }

        ///<summary>
        ///工作时段开始
        ///</summary>
        [DBColumn(ColumnName = "WORKSTART", DataType = DbType.String)]
        public string WORKSTART { get; set; }

        ///<summary>
        ///工作时段结束
        ///</summary>
        [DBColumn(ColumnName = "WORKEND", DataType = DbType.String)]
        public string WORKEND { get; set; }

        ///<summary>
        ///班次
        ///</summary>
        [DBColumn(ColumnName = "SCHEDULINGORDER", DataType = DbType.String)]
        public string SCHEDULINGORDER { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///是否停用
        ///</summary>
        [DBColumn(ColumnName = "FLGACTIVE", DataType = DbType.String)]
        public string FLGACTIVE { get; set; }

        ///<summary>
        ///删除标识
        ///</summary>
        [DBColumn(ColumnName = "FLGDEL", DataType = DbType.String)]
        public string FLGDEL { get; set; }

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

        ///<summary>
        ///分厂名称
        ///</summary>
        public string FNAME { get; set; }

        ///<summary>
        ///流水线名称
        ///</summary>
        public string PLNAME { get; set; }

        ///<summary>
        ///相关班组
        ///</summary>
        public string ENAME { get; set; }
    }
}
