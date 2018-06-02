using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;
using System.Collections.Generic;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产品BOM信息
    ///　作    者：wanglu
    ///　编写日期：2018年01月01日
    /// </summary>
    [DBTable(TableName = "T_FP_PRODUCEBOM")]
    public class ProduceBOM : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///产品主键
        ///</summary>
        [DBColumn(ColumnName = "PRODUCEID", DataType = DbType.String)]
        public string PRODUCEID { get; set; }

        ///<summary>
        ///生产量
        ///</summary>
        [DBColumn(ColumnName = "Amount", DataType = DbType.Int64)]
        public int Amount { get; set; }

        ///<summary>
        ///主计量单位
        ///</summary>
        [DBColumn(ColumnName = "MainUnit", DataType = DbType.String)]
        public string MainUnit { get; set; }

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
        ///产品名称
        ///</summary>
        public string PRODUCENAME { get; set; }

        /// <summary>
        /// 产品BOM明细
        /// </summary>
        public List<BOMDetail> Details { get; set; }
    }
}
