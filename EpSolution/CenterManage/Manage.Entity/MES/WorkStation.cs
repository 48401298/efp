using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：工位
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_WORKSTATION")]
    public class WorkStation : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///工位编号
        ///</summary>
        [DBColumn(ColumnName = "WSCODE", DataType = DbType.String)]
        public string WSCODE { get; set; }

        ///<summary>
        ///工位名称
        ///</summary>
        [DBColumn(ColumnName = "WSNAME", DataType = DbType.String)]
        public String WSNAME { get; set; }

        ///<summary>
        ///所属工厂主键
        ///</summary>
        [DBColumn(ColumnName = "FACTORYPID", DataType = DbType.String)]
        public string FACTORYPID { get; set; }

        ///<summary>
        ///所属生产线主键
        ///</summary>
        [DBColumn(ColumnName = "PRODUCTLINEPID", DataType = DbType.String)]
        public string PRODUCTLINEPID { get; set; }

        ///<summary>
        ///相关设备主键
        ///</summary>
        [DBColumn(ColumnName = "EQUIPMENTPID", DataType = DbType.String)]
        public string EQUIPMENTPID { get; set; }

        ///<summary>
        ///负责人
        ///</summary>
        [DBColumn(ColumnName = "PERSONINCHARGE", DataType = DbType.String)]
        public string PERSONINCHARGE { get; set; }

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
        ///相关设备
        ///</summary>
        public string ENAME { get; set; }
    }
}
