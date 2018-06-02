using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Inspect
{
    /// <summary>
    /// 监测项目
    /// </summary>
    [Serializable]
    [DBTable(TableName = "InspectItemInfo")]
    public class InspectItemEntity : BaseEntity 
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [DBColumn(ColumnName = "Id", DataType = DbType.String, IsKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [DBColumn(ColumnName = "ItemCode", DataType = DbType.String)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [DBColumn(ColumnName = "ItemName", DataType = DbType.String)]
        public string ItemName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [DBColumn(ColumnName = "Unit", DataType = DbType.String)]
        public string Unit { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        [DBColumn(ColumnName = "PointCount", DataType = DbType.Int32)]
        public int PointCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UpdateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }
}
