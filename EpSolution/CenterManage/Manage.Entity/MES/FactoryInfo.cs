using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：工厂信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月25日
    /// </summary>
    [DBTable(TableName = "T_FP_FACTORYINFO")]
    public class FactoryInfo : BaseEntity
    {
        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///工厂编号
        ///</summary>
        [DBColumn(ColumnName = "PCODE", DataType = DbType.String)]
        public string PCODE { get; set; }

        ///<summary>
        ///工厂名称
        ///</summary>
        [DBColumn(ColumnName = "PNAME", DataType = DbType.String)]
        public String PNAME { get; set; }

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
    }
}
