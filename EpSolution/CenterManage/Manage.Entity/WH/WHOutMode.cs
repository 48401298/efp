using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// </summary>
    ///　模块名称：出库方式
    ///　作    者：
    ///　编写日期：2017年07月02日
    /// </summary>
    [DBTable(TableName = "T_WH_OutMode")]
    public class WHOutMode : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DBColumn(ColumnName = "Description", DataType = DbType.String)]
        public string Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

    }

}
